using Abp.AutoMapper;
using bonolo.shesha.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.Incidents.Dto
{
    [AutoMap(typeof(Incident))]
    public class IncidentDto : CreateIncidentDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IncidentType { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime DateReported { get; set; }

        public Guid? JobCardId { get; set; }
    }
}
