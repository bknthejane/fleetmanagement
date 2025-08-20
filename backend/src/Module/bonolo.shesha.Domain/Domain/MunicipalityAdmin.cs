using Shesha.Domain;
using Shesha.Domain.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace bonolo.shesha.Domain.Domain
{
    [Entity(TypeShortAlias = "shesha.MunicipalityAdmin")]
    public class MunicipalityAdmin : Person
    {
        [InverseProperty("ContactPersonId")]
        public virtual Municipality? Municipality { get; set; }
    }
}
