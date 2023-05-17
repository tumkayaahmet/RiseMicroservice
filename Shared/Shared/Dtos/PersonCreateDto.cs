using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class PersonCreateDto : BaseClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; } 

    }
}
