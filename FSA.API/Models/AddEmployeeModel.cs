using FSA.API.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace FSA.API.Models
{
    public class AddEmployeeModel : IAddEmployee
    {
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string MiddleName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
    }
}
