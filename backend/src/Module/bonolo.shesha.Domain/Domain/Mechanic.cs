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
    [Entity(TypeShortAlias = "shesha.Mechanic")]
    public class Mechanic : Person
    {
        public virtual string Department { get; set; }

        //public virtual Guid SupervisorId { get; set; }
        [Column("shesha_SupervisorId")]
        public virtual Supervisor Supervisor { get; set; }

        //public virtual Guid MunicipalityId { get; set; }
        [ForeignKey("shesha_MunicipalityId")]
        //public virtual string MunicipalityName { get; set; }
        public virtual Municipality Municipality { get; set; }

        //public virtual Guid? AssignedJobCardId { get; set; }
        //[ForeignKey("AssignedJobCardId")]
        //public virtual JobCard AssignedJobCard { get; set; }
        //public virtual string AssignedJobCardNumber { get; set; }
    }
}
