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
    public class CreateJobCardDto
    {
        public Guid IncidentId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public Guid? SupervisorId { get; set; }
        public string Notes { get; set; }
    }
}
