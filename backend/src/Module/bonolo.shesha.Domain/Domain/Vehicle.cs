using Abp.Domain.Entities.Auditing;
using Shesha.Domain;
using Shesha.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Domain.Domain
{
    [Entity(TypeShortAlias = "shesha.Vehicle")]
    public class Vehicle : FullAuditedEntity<Guid>
    {
        public virtual string FleetNumber { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual string Model { get; set; }
        public virtual string Make { get; set; }
        public virtual DateTime LicenseExpiry { get; set; }
        public virtual bool IsActive { get; set; } = true;

        //public virtual Guid MunicipalityId { get; set; }
        [ForeignKey("MunicipalityId")]
        public virtual Municipality Municipality { get; set; }
        public virtual string MunicipalityName { get; set; }

        //public virtual Guid? AssignedDriverId { get; set; }
        [ForeignKey("AssignedDriverId")]
        public virtual Driver AssignedDriver { get; set; }

        public virtual string AssignedDriverName { get; set; }
    }
}
