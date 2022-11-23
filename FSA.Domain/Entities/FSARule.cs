using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Domain.Entities
{
    public class FSARule
    {
        public int ID { get; set; }
        public decimal FSALimit { get; set; }
        public int YearCoverage { get; set; }

       
    }
}
