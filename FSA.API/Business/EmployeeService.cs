using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.Data.Repository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<IEmployee> GetEmployees()
        {
            
            var dbEmployees = _employeeRepository.GetList().ToList();
            var employees = new List<IEmployee>();
            dbEmployees.ForEach(e => employees.Add(new EmployeeModel { ID = e.ID, Name = e.FirstName + " " + e.LastName }));
            return employees;
        }
    }
}