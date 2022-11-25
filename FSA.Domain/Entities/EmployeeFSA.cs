using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Domain.Entities
{
    public class EmployeeFSA
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int FSAID { get; set; }
    }
}
