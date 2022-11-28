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
    public class APIEmployeeClaimLogic
    {
        private ClaimsBusinessLogic _logic;
        private const int _employeeID = 2;// CHANGE EMPLOYEE ID
        public APIEmployeeClaimLogic()
        {
            _logic = new ClaimsBusinessLogic(_employeeID);
        }

        [Fact]
        public void AddClaim()
        {
            var result = _logic.AddClaim(new TransactClaim
            {
                ReceiptDate = "02/15/2022",
                ReceiptAmount = 3000,
                ClaimAmount = 2000,
                ReceiptNumber = "1647IJFTHJT",
                ReferenceNumber = "1647IJFTHJT435467"
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
                ReceiptDate = "09/18/2022",
                ReceiptAmount = 2000,
                ClaimAmount = 2000,
                ReceiptNumber = "TDYKTX45YT544",
                ReferenceNumber = "TDYKTX45YT544456653425"
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