using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Domain.Domain
{
    public class Incident : FullAuditedEntity<Guid>
    {
        public string Description { get; set; }
        public string IncidentType { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime DateReported { get; set; } = DateTime.UtcNow;

        public Guid VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        public Guid DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }

        public Guid JobCardId { get; set; }
        [ForeignKey("JobCardId")]
        public virtual JobCard JobCard { get; set; }

        public Guid MunicipalityId { get; set; }
        [ForeignKey("MunicipalityId")]

        public virtual string MunicipalityName { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}
