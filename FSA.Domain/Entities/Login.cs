using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Domain.Entities
{
    public class Login
    {
        public int EmployeeID { get; set; }
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

    }
}
