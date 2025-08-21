using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper.Internal.Mappers;
using bonolo.shesha.Common.Services.JobCards.Dto;
using bonolo.shesha.Domain.Domain;
using Shesha;
using Shesha.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.JobCards
{
    public class JobCardAppService : SheshaAppServiceBase, IJobCardAppService
    {
        private readonly IRepository<JobCard, Guid> _jobCardRepository;
        private readonly IRepository<Incident, Guid> _incidentRepository;
        private readonly IRepository<Mechanic, Guid> _mechanicRepository;

        public JobCardAppService(
            IRepository<JobCard, Guid> jobCardRepository,
            IRepository<Incident, Guid> incidentRepository,
            IRepository<Mechanic, Guid> mechanicRepository)
        {
            _jobCardRepository = jobCardRepository;
            _incidentRepository = incidentRepository;
            _mechanicRepository = mechanicRepository;
        }

        public async Task<List<JobCardDto>> GetAllAsync()
        {
            var entities = await _jobCardRepository
                .GetAllIncluding(j => j.AssignedMechanic)
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<JobCardDto>>(entities);

            foreach (var dto in dtos)
            {
                var jobCard = entities.First(j => j.Id == dto.Id);
                dto.AssignedMechanicName = jobCard.AssignedMechanic != null
                    ? $"{jobCard.AssignedMechanic.FirstName} {jobCard.AssignedMechanic.LastName}"
                    : null;
            }

            return dtos;
        }


        public async Task<JobCardDto> GetAsync(Guid id)
        {
            var entity = await _jobCardRepository.GetAsync(id);
            return ObjectMapper.Map<JobCardDto>(entity);
        }

        public async Task<JobCardDto> CreateAsync(CreateJobCardDto input)
        {
            var jobCard = ObjectMapper.Map<JobCard>(input);
            jobCard.Id = Guid.NewGuid();
            jobCard.JobCardNumber = $"JC-{DateTime.UtcNow:yyyyMMddHHmmss}";
            jobCard.Status = "Open";
            jobCard.DateOpened = DateTime.UtcNow;

            await _jobCardRepository.InsertAsync(jobCard);
            return ObjectMapper.Map<JobCardDto>(jobCard);
        }

        public async Task<JobCardDto> UpdateAsync(JobCardDto input)
        {
            var jobCard = await _jobCardRepository.GetAsync(input.Id);
            var oldStatus = jobCard.Status;

            ObjectMapper.Map(input, jobCard);

            if (jobCard.AssignedMechanicId.HasValue)
            {
                var mechanic = await _mechanicRepository
                    .FirstOrDefaultAsync(m => m.Id == jobCard.AssignedMechanicId.Value);

                jobCard.AssignedMechanicName = mechanic != null
                    ? $"{mechanic.FirstName} {mechanic.LastName}"
                    : null;
            }
            else
            {
                jobCard.AssignedMechanicName = null;
            }

            switch (input.Status)
            {
                case "Assigned":
                    jobCard.Status = "Assigned";
                    if (jobCard.IncidentId != Guid.Empty)
                    {
                        var incident = await _incidentRepository.GetAsync(jobCard.IncidentId);
                        incident.Status = "In Progress";
                        await _incidentRepository.UpdateAsync(incident);
                    }
                    break;

                case "Done":
                    jobCard.Status = "Done";
                    jobCard.DateCompleted = DateTime.UtcNow;
                    if (jobCard.IncidentId != Guid.Empty)
                    {
                        var incident = await _incidentRepository.GetAsync(jobCard.IncidentId);
                        incident.Status = "In Progress";
                        await _incidentRepository.UpdateAsync(incident);
                    }
                    break;

                case "Closed":
                    jobCard.Status = "Closed";
                    jobCard.DateCompleted = DateTime.UtcNow;
                    if (jobCard.IncidentId != Guid.Empty)
                    {
                        var incident = await _incidentRepository.GetAsync(jobCard.IncidentId);
                        incident.Status = "Resolved";
                        await _incidentRepository.UpdateAsync(incident);
                    }
                    break;

                default:
                    break;
            }

            await _jobCardRepository.UpdateAsync(jobCard);
            return ObjectMapper.Map<JobCardDto>(jobCard);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _jobCardRepository.DeleteAsync(id);
        }
    }
}
