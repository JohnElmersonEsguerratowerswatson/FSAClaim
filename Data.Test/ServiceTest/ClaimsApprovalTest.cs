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

    public class ClaimsApprovalTest
    {
        private Mock<IRepository<Employee>> _employeeRepository;
        private Mock<IRepository<FSAClaim>> _fsaClaimRepository;
        private ClaimsApprovalLogic _logic;

        private List<FSAClaim> _fsaClaims;

        public ClaimsApprovalTest()
        {
            _employeeRepository = new Mock<IRepository<Employee>>();
            _fsaClaimRepository = new Mock<IRepository<FSAClaim>>();
            _fsaClaims = new List<FSAClaim>();
            _logic = new ClaimsApprovalLogic(_employeeRepository.Object, _fsaClaimRepository.Object);
            Setup();
        }

        private void Setup()
        {
            _employeeRepository.Setup(e => e.GetList()).Returns(new List<Employee>());
            _fsaClaimRepository.Setup(f => f.GetList(It.IsAny<Func<FSAClaim, bool>>())).Returns(GeneratePendingFSAClaims());
            _fsaClaimRepository.Setup(f => f.Update(It.IsAny<string>(), It.IsAny<Func<FSAClaim, bool>>())).Returns(new ClaimRepositoryResult(true));
        }



        private List<FSAClaim> GeneratePendingFSAClaims()
        {
            if (_fsaClaims.Count > 0) return _fsaClaims;
            var fixture = new Fixture();
            var claims = fixture.Create<List<FSAClaim>>();
            claims.ForEach(c =>
            {
                c.Status = "Pending";
            }
            );
            _fsaClaims = claims;
            return claims;
        }

        private ClaimApproval GenerateValidClaimApproval(bool approve)
        {
            var fixture = new Fixture();
            return new ClaimApproval { Approve = approve, ReferenceNumber = fixture.Create<string>() };

        }

        [Fact]
        public void Approve_Should_Return_SuccessResult()
        {
            var result = _logic.ApproveClaim(GenerateValidClaimApproval(true));
            Assert.True(result);
        }

        [Fact]
        public void Deny_Should_Return_SuccessResult()
        {
            var result = _logic.ApproveClaim(GenerateValidClaimApproval(false));
            Assert.True(result);
        }

        [Fact]
        public void GetList_Should_Return_PendingFSAClaimList()
        {
            var result = _logic.GetTableView();
            result.ForEach(r =>
            {
                Assert.True(r.Status == "Pending");
            });

        }
    }
}