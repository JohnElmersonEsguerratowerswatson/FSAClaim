using FSA.API.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.API.Tests
{
    public class APIClaimApprovalLogic
    {
        private ClaimsApprovalLogic _claimsApprovalLogic;
        public APIClaimApprovalLogic()
        {
            _claimsApprovalLogic = new ClaimsApprovalLogic();
        }

        [Fact]
        public void GetClaimsForApproval()
        {
            var claims = _claimsApprovalLogic.GetTableView();
            Assert.NotEmpty(claims);
        }

        [Fact]
        public void ApproveClaim()
        {
            bool success = _claimsApprovalLogic.ApproveClaim(new FSA.API.Models.ClaimApproval { Approve = true, ReferenceNumber = "G90N8HT9" });
            Assert.True(success);
        }
    }
}
