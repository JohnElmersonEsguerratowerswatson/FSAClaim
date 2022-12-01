using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<IEmployee> GetEmployees()
        {
            TRepository<Employee> repository = new TRepository<Employee>();
            var dbEmployees = repository.GetList().ToList();
            var employees = new List<IEmployee>();
            dbEmployees.ForEach(e => employees.Add(new EmployeeModel { ID = e.ID, Name = e.FirstName + " " + e.LastName }));
            return employees;
        }
    }
}