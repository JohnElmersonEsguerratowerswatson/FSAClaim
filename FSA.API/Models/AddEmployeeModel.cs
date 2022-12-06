using FSA.API.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace FSA.API.Models
{
    public class AddEmployeeModel : IAddEmployee
    {
        [StringLength(25),MinLength(1)]
        public string FirstName { get; set; }
        [StringLength(25), MinLength(1)]
        public string MiddleName { get; set; }
        [StringLength(25), MinLength(1)]
        public string LastName { get; set; }
    }
}
