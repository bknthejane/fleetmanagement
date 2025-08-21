using Abp.Application.Services;
using bonolo.shesha.Common.Services.Incidents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.Incidents
{
    public interface IIncidentAppService : IApplicationService
    {
        Task<IncidentDto> GetAsync(Guid id);
        Task<List<IncidentDto>> GetAllAsync();
        Task<IncidentDto> CreateAsync(CreateIncidentDto input);
        Task<IncidentDto> UpdateAsync(UpdateIncidentDto input);
        Task DeleteAsync(Guid id);

        Task<List<IncidentTypeDto>> GetIncidentTypesAsync();
    }
}
