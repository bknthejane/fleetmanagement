using Abp.AutoMapper;
using bonolo.shesha.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.JobCards.Dto
{
    [AutoMap(typeof(JobCard))]
    public class UpdateJobCardDto : CreateJobCardDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    }
}
