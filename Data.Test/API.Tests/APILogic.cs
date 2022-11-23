using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSA.API.Business;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.API.Business.Interfaces;
using Xunit;
using FSA.API.Models;

namespace FSA.Test.API.Tests
{
    public class APILogic
    {
        private ClaimsBusinessLogic _logic;

        public APILogic()
        {
            _logic = new ClaimsBusinessLogic(1);
        }

        [Fact]
        public void AddClaim()
        {
            var result = _logic.AddClaim(new TransactClaim
            {
                ReceiptDate = "02/14/2022",
                ReceiptAmount = 2000,
                ClaimAmount = 2000,
                ReceiptNumber = "002FVE03F",
                ReferenceNumber = ""
            });
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void UpdateClaim()
        {
            var result = _logic.Update(new TransactClaim
            {
                ReceiptDate = "02/12/2022",
                ReceiptAmount = 2000,
                ClaimAmount = 200,
                ReceiptNumber = "RBF0103",
                ReferenceNumber = "2022MM23125r"
            });
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteClaim()
        {
            var result = _logic.Delete(new TransactClaim
            {
                ReceiptDate = "02/12/2022",
                ReceiptAmount = 2000,
                ClaimAmount = 200,
                ReceiptNumber = "RBF0103",
                ReferenceNumber = "2022MM12RBF0103"
            });
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetClaims()
        {
            var claims = _logic.GetClaimList();
            Assert.NotEmpty(claims);
        }

        [Fact]
        public void GetClaim()
        {
            var claim = _logic.GetClaim("2022MM12RBF0103");
            Assert.NotNull(claim);
        }
    }
}