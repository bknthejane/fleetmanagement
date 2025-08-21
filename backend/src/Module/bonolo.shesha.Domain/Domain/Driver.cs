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
    [Entity(TypeShortAlias = "shesha.Driver")]
    public class Driver : Person
    {
        public virtual string LicenseNumber { get; set; }
        public virtual DateTime? LicenseExpiryDate { get; set; }
        public virtual Municipality Municipality { get; set; }

        //public virtual Guid? AssignedVehicleId { get; set; }

        [Column("shesha_AssignedVehicleId")]
        public virtual Vehicle AssignedVehicle { get; set; }
        [Column("shesha_AssignedVehicleFleetNumber")]
        public virtual string AssignedVehicleFleetNumber { get; set; }
    }
}
