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
    public class FSARuleServiceTest
    {

        private Mock<IRepository<Employee>> _employeeRepository;
        private Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>> _employeeFSARepository;
        private Mock<IRepository<FSARule>> _fsaRuleRepository;
        private Mock<ITransactAssociateEntityRepository<Employee, EmployeeFSA, FSARule>> _employeeFSATransactRepository;
        private Fixture _fixture;

        private FSARuleLogic _logic;
        private int _employeeID = 6;

        public FSARuleServiceTest()
        {
            _employeeFSARepository = new Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>>();
            _employeeRepository = new Mock<IRepository<Employee>>();
            _fsaRuleRepository = new Mock<IRepository<FSARule>>();
            _employeeFSATransactRepository = new Mock<ITransactAssociateEntityRepository<Employee, EmployeeFSA, FSARule>>();
            //INSTANCE OF SUT
            _logic = new FSARuleLogic(_employeeRepository.Object, _employeeFSARepository.Object,
                                      _fsaRuleRepository.Object, _employeeFSATransactRepository.Object);
            _fixture = new Fixture();


        }



        private Employee GenerateEmployee()
        {
           
            var employee = _fixture.Create<Employee>();
            employee.ID = _employeeID;

            return employee;
        }

        private FSARule GenerateFSARule(bool isNull = false)
        {
            if (isNull) return null;
            
            var rule = _fixture.Create<FSARule>();
            return rule;

        }

        private TransactFSARule GenerateValidTransactFSARule()
        {
           
            var rule = _fixture.Create<TransactFSARule>();
            return rule;
        }

        private void Setup()
        {
            _employeeRepository.Setup(e => e.Get(It.IsAny<Func<Employee, bool>>())).Returns(GenerateEmployee());
            _employeeFSARepository.Setup(efsa => efsa.Get(It.IsAny<Func<Employee, bool>>())).Returns(GenerateFSARule(true));
            _fsaRuleRepository.Setup(r => r.Get(It.IsAny<Func<FSARule, bool>>())).Returns(GenerateFSARule());//CURRENT EMPLOYEE HAS NO FSA
            _employeeFSATransactRepository.Setup(r => r.Add(It.IsAny<FSARule>(), It.IsAny<int>())).Returns(new ClaimRepositoryResult(true));
        }

        [Fact]
        public void AddFSARule_Should_Return_SuccessResult()
        {
            Setup();
            _logic.EmployeeID = _employeeID; // initialize property for current employee
            var result = _logic.AddFSARule(GenerateValidTransactFSARule());
            Assert.True(result.IsSuccess);
        }

    }
}