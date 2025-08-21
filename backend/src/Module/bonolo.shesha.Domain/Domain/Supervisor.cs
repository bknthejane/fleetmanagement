using Shesha.Domain;
using Shesha.Domain.Attributes;
using System;
using System.Collections.Generic;

namespace bonolo.shesha.Domain.Domain
{
    [Entity(TypeShortAlias = "shesha.Supervisor")]
    public class Supervisor : Person
    {
        public virtual string Department { get; set; }
        public virtual Municipality Municipality { get; set; }

        public virtual IList<JobCard> JobCards { get; set; } = new List<JobCard>();
    }
}
