using FSA.API.Business.Interfaces;
using FSA.API.Models;
using FSA.API.Models.Interface;

namespace FSA.API.Business.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<IEmployee> GetEmployees();
        public IAddEmployeeResult Add(AddEmployeeModel employee);

    }
}