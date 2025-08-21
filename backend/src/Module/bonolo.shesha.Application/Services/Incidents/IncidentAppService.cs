using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using bonolo.shesha.Common.Services.Incidents.Dto;
using bonolo.shesha.Domain.Domain;
using Shesha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.Incidents
{
    public class IncidentAppService : SheshaAppServiceBase, IIncidentAppService
    {
        private readonly IRepository<Incident, Guid> _incidentRepository;
        private readonly IRepository<JobCard, Guid> _jobCardRepository;
        private readonly IRepository<Supervisor, Guid> _supervisorRepository;

        // Incident-to-department mapping
        private static readonly List<(string Type, string Department)> IncidentTypes = new()
        {
            ("Engine Failure", "Maintenance"),
            ("Body Damage", "Fabrication"),
            ("Electrical Issue", "Diverse Workshop Support"),
            ("Routine Service", "Maintenance"),
            ("Flat Tire", "Maintenance"),
            ("Tire Replacement", "Maintenance"),
            ("Wheel Alignment", "Maintenance")
        };

        public IncidentAppService(
            IRepository<Incident, Guid> incidentRepository,
            IRepository<JobCard, Guid> jobCardRepository,
            IRepository<Supervisor, Guid> supervisorRepository)
        {
            _incidentRepository = incidentRepository;
            _jobCardRepository = jobCardRepository;
            _supervisorRepository = supervisorRepository;
        }

        // Get all incidents
        public async Task<List<IncidentDto>> GetAllAsync()
        {
            var entities = await _incidentRepository.GetAllListAsync();
            return ObjectMapper.Map<List<IncidentDto>>(entities);
        }

        // Get single incident by Id
        public async Task<IncidentDto> GetAsync(Guid id)
        {
            var entity = await _incidentRepository.GetAsync(id);
            return ObjectMapper.Map<IncidentDto>(entity);
        }

        // Get list of all incident types with their mapped departments
        public async Task<List<IncidentTypeDto>> GetIncidentTypesAsync()
        {
            return IncidentTypes
                .Select(t => new IncidentTypeDto
                {
                    Type = t.Type,
                    Department = t.Department
                })
                .ToList();
        }

        // Create incident with auto department mapping and job card creation
        [UnitOfWork]
        public async Task<IncidentDto> CreateAsync(CreateIncidentDto input)
        {
            try
            {
                // 1. Map incident type to department
                var mapping = IncidentTypes.FirstOrDefault(t => t.Type.Equals(input.IncidentType, StringComparison.OrdinalIgnoreCase));
                if (mapping == default)
                    throw new UserFriendlyException($"Invalid Incident Type: {input.IncidentType}");

                // 2. Create incident entity
                var incident = ObjectMapper.Map<Incident>(input);
                incident.Id = Guid.NewGuid();
                incident.Department = mapping.Department;
                incident.DateReported = DateTime.UtcNow;

                // Insert and save incident first
                var insertedIncident = await _incidentRepository.InsertAsync(incident);
                await CurrentUnitOfWork.SaveChangesAsync();

                // 3. Find supervisor with better error handling
                Logger.Info($"Searching supervisor for Dept='{mapping.Department}' and Municipality='{input.MunicipalityId}'");

                var supervisor = await _supervisorRepository.FirstOrDefaultAsync(
                    s => s.Department.ToLower().Trim() == mapping.Department.ToLower().Trim()
                         && s.Municipality.Id == input.MunicipalityId);

                if (supervisor == null)
                {
                    var allSupervisors = await _supervisorRepository.GetAllListAsync();
                    Logger.Warn($"No supervisor found for department '{mapping.Department}'. Available supervisors: {string.Join(", ", allSupervisors.Select(s => $"{s.Department}-{s.Municipality?.Id}"))}");
                    throw new UserFriendlyException($"No supervisor found for department: {mapping.Department}");
                }

                // 4. Create JobCard with proper foreign key references
                var jobCard = new JobCard
                {
                    Id = Guid.NewGuid(),
                    JobCardNumber = $"JC-{DateTime.UtcNow:yyyyMMddHHmmss}",
                    IncidentId = insertedIncident.Id, // Use the inserted incident's ID
                    VehicleId = input.VehicleId,
                    DriverId = input.DriverId,
                    SupervisorId = supervisor.Id, // Set the foreign key explicitly
                    Status = "Open",
                    Notes = $"Job card auto-created from incident: {incident.Description ?? "No additional notes"}",
                    Priority = "Medium",
                    DateOpened = DateTime.UtcNow
                };

                // Don't set navigation properties, only foreign keys
                // jobCard.Supervisor = supervisor; // Remove this line
                // jobCard.Incident = incident;     // Remove this line if it exists

                await _jobCardRepository.InsertAsync(jobCard);
                await CurrentUnitOfWork.SaveChangesAsync();

                // 5. Return DTO
                var dto = ObjectMapper.Map<IncidentDto>(insertedIncident);
                dto.JobCardId = jobCard.Id;

                return dto;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error creating incident: {ex.Message}", ex);
                throw;
            }
        }

        // Update incident and job card supervisor assignment
        [UnitOfWork]
        public async Task<IncidentDto> UpdateAsync(UpdateIncidentDto input)
        {
            try
            {
                var entity = await _incidentRepository.GetAsync(input.Id);

                var mapping = IncidentTypes.FirstOrDefault(t => t.Type.Equals(input.IncidentType, StringComparison.OrdinalIgnoreCase));
                if (mapping == default)
                    throw new UserFriendlyException("Invalid Incident Type");

                entity.Department = mapping.Department;
                ObjectMapper.Map(input, entity);

                var supervisor = await _supervisorRepository.FirstOrDefaultAsync(
                    s => s.Department.ToLower().Trim() == mapping.Department.ToLower().Trim()
                         && s.Municipality.Id == input.MunicipalityId);

                if (supervisor == null)
                    throw new UserFriendlyException($"No supervisor found for department: {mapping.Department}");

                await _incidentRepository.UpdateAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                // Update job card in separate operation
                var jobCard = await _jobCardRepository.FirstOrDefaultAsync(jc => jc.IncidentId == entity.Id);

                if (jobCard != null)
                {
                    jobCard.SupervisorId = supervisor.Id; // Use foreign key instead of navigation property
                    await _jobCardRepository.UpdateAsync(jobCard);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }

                var dto = ObjectMapper.Map<IncidentDto>(entity);
                if (jobCard != null)
                    dto.JobCardId = jobCard.Id;

                return dto;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error updating incident: {ex.Message}", ex);
                throw;
            }
        }

        // Delete incident
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                // Check if there are related job cards first
                var relatedJobCards = await _jobCardRepository.GetAllListAsync(jc => jc.IncidentId == id);

                // Delete related job cards first if they exist
                foreach (var jobCard in relatedJobCards)
                {
                    await _jobCardRepository.DeleteAsync(jobCard);
                }

                await _incidentRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error deleting incident: {ex.Message}", ex);
                throw;
            }
        }

        // Optional: Get department for a given incident type
        public async Task<string> GetDepartmentByIncidentTypeAsync(string incidentType)
        {
            var mapping = IncidentTypes.FirstOrDefault(t => t.Type.Equals(incidentType, StringComparison.OrdinalIgnoreCase));
            if (mapping == default)
                throw new UserFriendlyException("Invalid Incident Type");

            return mapping.Department;
        }
    }
}