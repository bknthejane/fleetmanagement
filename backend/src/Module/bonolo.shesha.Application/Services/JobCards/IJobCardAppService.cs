using Abp.Application.Services;
using bonolo.shesha.Common.Services.JobCards.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.JobCards
{
    public interface IJobCardAppService : IApplicationService
    {
        Task<List<JobCardDto>> GetAllAsync();
        Task<JobCardDto> GetAsync(Guid id);
        Task<JobCardDto> CreateAsync(CreateJobCardDto input);
        Task<JobCardDto> UpdateAsync(JobCardDto input);
        Task DeleteAsync(Guid id);
    }
}
