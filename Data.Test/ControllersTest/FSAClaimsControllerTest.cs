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
        private Fixture _fixture;

        public FSAClaimsControllerTest()
        {
            _fixture = new Fixture();
            _service = new Mock<IFSAClaimBusinessService>();
            _claimsController = new ClaimsController(_service.Object);
        }
        


        private List<ViewClaim> GenerateViewClaimList()
        {
           
            return _fixture.Create<List<ViewClaim>>();
        }

        private GetClaimsResult GenerateClaimResult()
        {
            var claimView = _fixture.Build<GetClaimsResult>().Without(c => c.Claims).Do(c => c.Claims = GenerateViewClaimList()).Create();
            return claimView;
        }

        private ClaimResult GenerateTransactClaimResult()
        {
            
            return _fixture.Build<ClaimResult>().Without(r => r.IsSuccess).Do(r => r.IsSuccess = true).Create();
        }

        private TransactClaim GenerateTransactClaim()
        {
           
            return _fixture.Create<TransactClaim>();
        }

        [Fact]
        public void GetList_Should_ReturnOK()
        {
            var mockedClaims = GenerateClaimResult();
            _service.Setup(s => s.GetClaimsResult()).Returns(mockedClaims);
            var claimViewResult = _claimsController.GetList();
            Assert.Equal(typeof(OkObjectResult) , claimViewResult.Result.GetType());
           // Assert.Equal(mockedClaims,claimViewResult.Result.Value);
           //  AssertThat(result, Is.TypeOf<OkResult>());


        }


        [Fact]
        public void Add_Should_ReturnOK()
        {
            _service.Setup(s => s.AddClaim(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            var claimViewResult = _claimsController.Create(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult.Result).Value).IsSuccess);
        }

        [Fact]
        public void Edit_Should_ReturnOK()
        {
            _service.Setup(s => s.Update(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            var claimViewResult = _claimsController.Edit(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.Result.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult.Result).Value).IsSuccess);
        }


        [Fact]
        public void Delete_Should_ReturnOK()
        {
            _service.Setup(s => s.Delete(It.IsAny<ITransactClaim>())).Returns(GenerateTransactClaimResult());
            var claimViewResult = _claimsController.Delete(GenerateTransactClaim());
            Assert.True(typeof(OkObjectResult) == claimViewResult.GetType());
            Assert.True(((ClaimResult)((OkObjectResult)claimViewResult).Value).IsSuccess);
        }
    }
}
