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
    [Entity(TypeShortAlias = "shesha.MunicipalityAdmin")]
    public class MunicipalityAdmin : Person
    {
        //public virtual string Username { get; set; }
        //public virtual string Password { get; set; }
        //public virtual string ConfirmPassword { get; set; }
        [InverseProperty("ContactPersonId")]
        public virtual Municipality? Municipality { get; set; }
    }
}
