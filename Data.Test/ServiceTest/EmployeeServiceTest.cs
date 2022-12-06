using AutoFixture;
using FSA.API.Business;
using FSA.API.Models;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
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
        private Fixture _fixture;
        public EmployeeServiceTest()
        {
            _employeeRepository = new Mock<IRepository<Employee>>();
            //INSTANCE OF SUT
            _employeeService = new EmployeeService(_employeeRepository.Object);

            _fixture = new Fixture();

        }


        private List<Employee> GenerateEmployeeList()
        {
            return _fixture.Create<List<Employee>>();
        }

        private AddEmployeeModel GenerateValidEmployeeModel()
        {
            return _fixture.Create<AddEmployeeModel>();
        }

        [Fact]
        public void GetList_Should_Return_EmployeeList()
        {
            _employeeRepository.Setup(r => r.GetList()).Returns(GenerateEmployeeList());
            var employees = _employeeService.GetEmployees();
            Assert.NotEmpty(employees);
        }

        

        [Fact]
        public void AddEmployee_Should_Return_SuccessResult()
        {
            _employeeRepository.Setup(r => r.Add(It.IsAny<Employee>())).Returns(new ClaimRepositoryResult(true));

            var result = _employeeService.Add(GenerateValidEmployeeModel());

            Assert.True(result.IsSuccess);
        }
    }
}
