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
    public class JobCardDto : CreateJobCardDto
    {
        public Guid Id { get; set; }
        public string JobCardNumber { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateCompleted { get; set; }
        public Guid? AssignedMechanicId { get; set; }
        public string AssignedMechanicName { get; set; }
    }
}
