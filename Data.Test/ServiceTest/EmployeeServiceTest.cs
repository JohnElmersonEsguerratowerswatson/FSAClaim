using AutoFixture;
using FSA.API.Business;
using FSA.Data.Repository;
using FSA.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.ServiceTest
{
    public class EmployeeServiceTest
    {

        private Mock<IRepository<Employee>> _employeeRepository;
        private EmployeeService _employeeService;
        public EmployeeServiceTest()
        {
            _employeeRepository = new Mock<IRepository<Employee>>();
            //INSTANCE OF SUT
            _employeeService = new EmployeeService(_employeeRepository.Object);
            Setup();
        }

        private void Setup()
        {
            _employeeRepository.Setup(r => r.GetList()).Returns(GenerateEmployeeList());
        }

        private List<Employee> GenerateEmployeeList()
        {
            var fixture = new Fixture();
            return fixture.Create<List<Employee>>();
        }

        [Fact]
        public void GetList_Should_Return_EmployeeList()
        {
            var employees = _employeeService.GetEmployees();
            Assert.NotEmpty(employees);
        }
    }
}
