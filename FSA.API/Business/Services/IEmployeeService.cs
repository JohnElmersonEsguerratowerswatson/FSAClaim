using FSA.API.Business.Interfaces;

namespace FSA.API.Business.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<IEmployee> GetEmployees();

    }
}