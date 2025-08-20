using Abp.Domain.Entities.Auditing;
using Shesha.Domain.Attributes;
using System;

namespace bonolo.shesha.Domain.Domain
{
    [Entity(TypeShortAlias = "shesha.Municipality")]
    public class Municipality : FullAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual MunicipalityAdmin? ContactPerson { get; set; }
    }

}
