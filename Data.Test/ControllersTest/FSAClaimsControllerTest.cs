using AutoFixture;
using FSA.API.Business;
using FSA.API.Controllers;
using FSA.API.Models;
using FSA.API.Models.Interface;
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
    public class FSAClaimsControllerTest
    {
        private Mock<IFSAClaimBusinessService> _service;
        private ClaimsController _claimsController;
        public FSAClaimsControllerTest()
        {
            _service = new Mock<IFSAClaimBusinessService>();
            _claimsController = new ClaimsController(_service.Object);
            Setup();
        }

        private void Setup()
        {
            _service.Setup(s => s.AddClaim(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            _service.Setup(s => s.Update(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            _service.Setup(s => s.Delete(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            _service.Setup(s => s.GetClaimsResult()).Returns(GenerateClaimResult());
        }

        private List<ViewClaim> GenerateViewClaimList()
        {
            var fixture = new Fixture();
            return fixture.Create<List<ViewClaim>>();
        }

        private GetClaimsResult GenerateClaimResult()
        {
            var fixture = new Fixture();
            var claimView = fixture.Build<GetClaimsResult>().Without(c => c.Claims).Do(c => c.Claims = GenerateViewClaimList()).Create();
            return claimView;
        }

        private ClaimResult GenerateTransactClaimResult()
        {
            var fixture = new Fixture();
            return fixture.Build<ClaimResult>().Without(r => r.IsSuccess).Do(r => r.IsSuccess = true).Create();
        }

        private TransactClaim GenerateTransactClaim()
        {
            var fixture = new Fixture();
            return fixture.Create<TransactClaim>();
        }

        [Fact]
        public void GetList_Should_ReturnOK()
        {
            var claimViewResult = _claimsController.GetList();
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
        }


        [Fact]
        public void Add_Should_ReturnOK()
        {
            var claimViewResult = _claimsController.Create(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult.Result).Value).IsSuccess);
        }


        [Fact]
        public void Edit_Should_ReturnOK()
        {
            var claimViewResult = _claimsController.Edit(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult.Result).Value).IsSuccess);
        }


        [Fact]
        public void Delete_Should_ReturnOK()
        {
            var claimViewResult = _claimsController.Delete(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult).Value).IsSuccess);
        }
    }
}
