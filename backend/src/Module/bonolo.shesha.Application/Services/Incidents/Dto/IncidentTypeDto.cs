using Abp.AutoMapper;
using bonolo.shesha.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonolo.shesha.Common.Services.Incidents.Dto
{
    [AutoMap(typeof(Incident))]
    public class IncidentTypeDto
    {
        public string Type { get; set; }
        public string Department { get; set; }
    }
}
