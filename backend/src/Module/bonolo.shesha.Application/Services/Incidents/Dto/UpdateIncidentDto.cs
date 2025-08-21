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
    public class UpdateIncidentDto : CreateIncidentDto
    {
        public Guid Id { get; set; }
        public string IncidentType { get; set; }
        public string Description { get; set; }
        public Guid MunicipalityId { get; set; }
        public string Status { get; set; }
    }
}
