using Xunit;
using FSA.Data;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Domain.Entities;


namespace FSA.Test.Data.Tests
{
    public class FSARepoTest
    {
        private FSAClaimRepository _repo;
        public FSARepoTest()
        {
            _repo = new FSAClaimRepository(true);
        }

        [Fact]
        public void GetList()
        {
            Assert.NotStrictEqual(0, _repo.GetList().Count);
        }

        [Fact]
        public void AddClaim()
        {
            FSAClaim claim = new FSAClaim
            {
                ApprovalDate = System.DateTime.MinValue,
                ClaimAmount = 100,
                DateSubmitted = System.DateTime.UtcNow,
                ReceiptAmount = 100,
                EmployeeID = 1,
                Modified = System.DateTime.UtcNow,
                ReceiptDate = System.DateTime.UtcNow,
                ReceiptNumber = "0001038JDJHV00",
                Status = "Pending",
                ReferenceNumber = "2022110001038JDJHV00"
            };
            var result = _repo.Add(claim);
            Assert.True(result.IsSuccess);
        }



    }
}