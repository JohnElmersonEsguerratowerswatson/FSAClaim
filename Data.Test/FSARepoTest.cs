using Xunit;
using FSA.Data;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Domain.Entities;


namespace Data.Test
{
    public class FSARepoTest
    {
        private FSAClaimRepository _repo;
        public FSARepoTest()
        {
            _repo = new FSAClaimRepository();
        }

        [Fact]
        public void GetList()
        {
            Assert.NotStrictEqual(0, _repo.GetList().Count);
        }

        [Fact]
        public void GetFSA()
        {
            

        }
    }
}