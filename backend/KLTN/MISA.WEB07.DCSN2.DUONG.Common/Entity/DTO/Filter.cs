using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.Common.Entity.DTO
{
    public class Filter
    {
        public string columnName { get; set; }
        public string filterValue { get; set; }
        public string operatorValue { get; set; }
    }
}
