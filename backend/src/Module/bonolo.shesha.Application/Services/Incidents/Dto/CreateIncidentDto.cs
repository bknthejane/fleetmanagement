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
    public class CreateIncidentDto
    {
        public string Description { get; set; }
        public string IncidentType { get; set; }
        public string Status { get; set; } = "Submitted";
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public Guid MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public string Department { get; set; }
    }
}
