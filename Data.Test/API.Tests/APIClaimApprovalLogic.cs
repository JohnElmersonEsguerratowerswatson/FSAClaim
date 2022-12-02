using FSA.API.Business;
using FSA.API.Models;
using Moq;
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
        //private ClaimsApprovalLogic _claimsApprovalLogic;
        private Mock<IClaimsApprovalService> _serviceMoq;
        private ClaimApproval? claimApproval;
        private ClaimApproval? claimApprovalDeny;
        public APIClaimApprovalLogic()
        {
            _serviceMoq = new Mock<IClaimsApprovalService>();
            // _claimsApprovalLogic = new ClaimsApprovalLogic();

            Setup();

        }

        private void Setup()
        {
            List<ClaimsApprovalTableItems> items = new List<ClaimsApprovalTableItems>();
            items.Add(new FSA.API.Models.ClaimsApprovalTableItems
            {
                ClaimAmount = 5000,
                DateSubmitted = DateTime.Now.ToString("MM/dd/yyyy"),
                EmployeeID = 1,
                EmployeeName = "John Doe",
                ReceiptAmount = 6000,
                ReceiptDate = DateTime.Now.ToString("MM/dd/yyyy"),
                ReceiptNumber = "V4RW4RW42",
                ReferenceNumber = "234354TBRE43",
                Status = "Pending",
                TotalClaimAmount = 5000
            });
            items.Add(new ClaimsApprovalTableItems
            {
                ClaimAmount = 4000,
                DateSubmitted = DateTime.Now.ToString("MM/dd/yyyy"),
                EmployeeID = 1,
                EmployeeName = "John Doe",
                ReceiptAmount = 6000,
                ReceiptDate = DateTime.Now.ToString("MM/dd/yyyy"),
                ReceiptNumber = "243567I654",
                ReferenceNumber = "23456J453H34H",
                Status = "Pending",
                TotalClaimAmount = 5000
            });
            _serviceMoq.Setup(m => m.GetTableView()).Returns(() => items);
            claimApproval = new ClaimApproval { Approve = true, ReferenceNumber = "234354TBRE43" };
            claimApprovalDeny = new ClaimApproval { Approve = false, ReferenceNumber = "23456J453H34H" };

            //Approve/Deny Success
            _serviceMoq.Setup(m => m.ApproveClaim(It.Is<ClaimApproval>(ca => items.Any(i=>i.ReferenceNumber== ca.ReferenceNumber)))).Returns(true);
            _serviceMoq.Setup(m => m.ApproveClaim(It.Is<ClaimApproval>(ca => items.Any(i => i.ReferenceNumber == ca.ReferenceNumber)))).Returns(true);


            // Assert.True(success);))
        }

        [Fact]
        public void GetClaimsForApproval()
        {

            var claims = _serviceMoq.Object.GetTableView();
            Assert.NotEmpty(claims);
        }

        [Fact]
        public void ApproveClaim()
        {
            bool success = _serviceMoq.Object.ApproveClaim(claimApproval);
            Assert.True(success);
        }

        [Fact]
        public void DenyClaim()
        {
            bool success = _serviceMoq.Object.ApproveClaim(claimApprovalDeny);
            Assert.True(success);
        }


        [Fact]
        public void ApproveClaim_Should_Fail()
        {
            bool success = _serviceMoq.Object.ApproveClaim(new ClaimApproval { Approve = true, ReferenceNumber = "324H34G23F4" });
            Assert.False(success);
        }

        [Fact]
        public void DenyClaim_Should_Fail()
        {
            bool success = _serviceMoq.Object.ApproveClaim(new ClaimApproval { Approve = false, ReferenceNumber = "324H34G23F4" });
            Assert.False(success);
        }

    }
}