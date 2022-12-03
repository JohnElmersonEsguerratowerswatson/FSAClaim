using AutoFixture;
using FSA.API.Business;
using FSA.API.Models;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
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

    public class ClaimsServiceTest
    {
        ClaimsBusinessLogic _logic;
        Mock<IRepository<FSAClaim>> _claimRepository;
        Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>> _employeeFSARepository;
        Mock<IRepository<Employee>> _employeeRepository;
        private int _employeeID = 6;

        private void Setup()
        {
            _claimRepository.Setup(r => r.Add(It.IsAny<FSAClaim>())).Returns(GenerateSuccessClaimResult());
            _claimRepository.Setup(r => r.Update(It.IsAny<FSAClaim>(), It.IsAny<Func<FSAClaim,bool>>())).Returns(GenerateSuccessClaimResult());
            _claimRepository.Setup(r => r.Delete(true, It.IsAny<Func<FSAClaim, bool>>())).Returns(GenerateSuccessClaimResult());
            _claimRepository.Setup(r => r.GetList()).Returns(GenerateFSAClaims());
            _claimRepository.Setup(r => r.GetList(It.IsAny<Func<FSAClaim,bool>>())).Returns(GenerateFSAClaims());

            _employeeFSARepository.Setup(r => r.GetList(It.IsAny<Func<Employee, bool>>())).Returns(GenerateFSARuleList());
            _employeeFSARepository.Setup(r => r.Get(It.IsAny<Func<Employee, bool>>())).Returns(GenerateFSARule());

            _employeeRepository.Setup(r => r.Get(It.IsAny<Func<Employee, bool>>())).Returns(GenerateEmployee());
        }



        public ClaimsServiceTest()
        {
            _claimRepository = new Mock<IRepository<FSAClaim>>();
            _employeeFSARepository = new Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>>();
            _employeeRepository = new Mock<IRepository<Employee>>();
            _logic = new ClaimsBusinessLogic(_claimRepository.Object, _employeeFSARepository.Object, _employeeRepository.Object);
            _logic.EmployeeID = _employeeID;
            Setup();
        }

        private List<FSAClaim> GenerateFSAClaims()
        {
            var fixture = new Fixture();
            var result = fixture.Create<List<FSAClaim>>();
            result.ForEach(x => {
                x.EmployeeID = _employeeID;
                x.Status = "Pending";
                x.DateSubmitted = DateTime.Now;
                x.ReceiptDate = DateTime.Now;
                x.ReceiptAmount += x.ClaimAmount;
                });
            return result;
        }

        private Employee GenerateEmployee()
        {
            var fixture = new Fixture();
            var result = fixture.Build<Employee>().Do(e => e.ID = _employeeID).Create();
            return result;
        }

        private ClaimRepositoryResult GenerateSuccessClaimResult()
        {
            var fixture = new Fixture();
            fixture.Register<ClaimRepositoryResult>(() => new ClaimRepositoryResult(true));
            var result = fixture.Create<ClaimRepositoryResult>();
            return result;
        }

        private ClaimRepositoryResult GenerateFailClaimResult()
        {
            var fixture = new Fixture();
            fixture.Register<ClaimRepositoryResult>(() => new ClaimRepositoryResult(false, "Failed transaction."));
            var result = fixture.Create<ClaimRepositoryResult>();
            return result;
        }

        private FSARule GenerateFSARule()
        {
            var fixture = new Fixture();
            var result = fixture.Create<FSARule>();
            return result;
        }
        private List<FSARule> GenerateFSARuleList()
        {
            var fixture = new Fixture();
            var result = fixture.Create<List<FSARule>>();
            return result;
        }

        private TransactClaim GenerateValidClaimInput()
        {
            var fixture = new Fixture();
            var claim = fixture.Create<TransactClaim>();
            claim.ReceiptAmount += claim.ClaimAmount;
            claim.ReceiptDate = "03/10/2022";
            return claim;
        }


        [Fact]
        public void GetList_Should_Return_SuccessResult()
        {

            var result = _logic.AddClaim(GenerateValidClaimInput());

            Assert.True(result.IsSuccess);
        }

    }
}
