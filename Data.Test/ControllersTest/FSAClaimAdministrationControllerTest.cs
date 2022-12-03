using AutoFixture;
using FSA.API.Business;
using FSA.API.Controllers;
using FSA.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.ControllersTest
{
    public class FSAClaimAdministrationControllerTest
    {
        private FSAClaimAdministrationController _controller;

        private Mock<IClaimsApprovalService> _service;

        public FSAClaimAdministrationControllerTest()
        {
            _service = new Mock<IClaimsApprovalService>();
            _controller = new FSAClaimAdministrationController(_service.Object);
            Setup();
        }

        private void Setup()
        {
            _service.Setup(s => s.ApproveClaim(It.IsAny<ClaimApproval>())).Returns(true);
            _service.Setup(s => s.GetTableView()).Returns(GenerateClaimsApprovalTableList());
        }

        private List<ClaimsApprovalTableItems> GenerateClaimsApprovalTableList()
        {
            var fixture = new Fixture();
            return fixture.Create<List<ClaimsApprovalTableItems>>();
        }
        private ClaimApproval GenerateClaimApproval(bool approve = true)
        {
            var fixture = new Fixture();
            return fixture.Build<ClaimApproval>().Without(a=>a.Approve).Do(a=>a.Approve= approve).Create();
        }

        [Fact]
        public void Should_return_all_claims()
        {
            var claimViewResult = _controller.Index();
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
        }

        [Fact]
        public void Approve_Should_return_success_result()
        {
            var claimViewResult = _controller.ClaimApproval(GenerateClaimApproval());
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
        }

        [Fact]
        public void Deny_Should_return_success_result()
        {
            var claimViewResult = _controller.ClaimApproval(GenerateClaimApproval(false));
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
        }
    }
}
