using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ReportDetailUpdateDto
    {
        public string Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportStatus { get; set; }
        public string ReportPath { get; set; }
    }
}
 
