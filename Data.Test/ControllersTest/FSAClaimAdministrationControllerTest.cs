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

        private Fixture _fixture;

        public FSAClaimAdministrationControllerTest()
        {
            _service = new Mock<IClaimsApprovalService>();
            _controller = new FSAClaimAdministrationController(_service.Object);
            _fixture = new Fixture();   
        }



        private List<ClaimsApprovalTableItems> GenerateClaimsApprovalTableList()
        {
           
            return _fixture.Create<List<ClaimsApprovalTableItems>>();
        }
        private ClaimApproval GenerateClaimApproval(bool approve = true)
        {
            
            return _fixture.Build<ClaimApproval>().Without(a=>a.Approve).Do(a=>a.Approve= approve).Create();
        }

        [Fact]
        public void Should_return_all_claims()
        {
            var claimsForApproval = GenerateClaimsApprovalTableList();
            _service.Setup(s => s.GetTableView()).Returns(claimsForApproval);
            var claimViewResult = _controller.Index();
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
            OkObjectResult okResult = (OkObjectResult)claimViewResult.Result;
            Assert.Equal(okResult.Value, claimsForApproval);
        }

        [Fact]
        public void Approve_Should_return_success_result()
        {
            _service.Setup(s => s.ApproveClaim(It.IsAny<ClaimApproval>())).Returns(true);
            var claimViewResult = _controller.ClaimApproval(GenerateClaimApproval());
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
        }

        [Fact]
        public void Deny_Should_return_success_result()
        {
            _service.Setup(s => s.ApproveClaim(It.IsAny<ClaimApproval>())).Returns(true);
            var claimViewResult = _controller.ClaimApproval(GenerateClaimApproval(false));
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
        }
    }
}
