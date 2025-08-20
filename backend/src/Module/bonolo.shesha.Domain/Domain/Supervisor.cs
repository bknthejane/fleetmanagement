using Shesha.Domain;
using Shesha.Domain.Attributes;
using System;

namespace bonolo.shesha.Domain.Domain
{
    [Entity(TypeShortAlias = "shesha.Supervisor")]
    public class Supervisor : Person
    {
        public virtual string Department { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}
