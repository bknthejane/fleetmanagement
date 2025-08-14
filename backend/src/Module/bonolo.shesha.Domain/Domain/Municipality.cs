using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Domain.Domain
{
    public class Municipality : FullAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Address { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string Email {  get; set; }
        public virtual string ContactNumber { get; set; }
    }
}
