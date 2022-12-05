using AutoFixture;
using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.DataTest
{
    public class FSAClaimsRepositoryTest
    {
        private FSAClaimRepository _repository;
        private FSAClaimContext _dbContext;


        private int _claimID;
        private int _employeeID;

        public FSAClaimsRepositoryTest()
        {
            DbContextOptionsBuilder<FSAClaimContext> optionsBuilder = new DbContextOptionsBuilder<FSAClaimContext>();
            optionsBuilder.UseInMemoryDatabase<FSAClaimContext>("FSAClaimsTestDB");
            _dbContext = new FSAClaimContext(optionsBuilder.Options);
            _repository = new FSAClaimRepository(_dbContext);

            Setup();
        }


        private void Setup()
        {
            DataTestHelper.SeedEmployees(_dbContext);
            DataTestHelper.SeedLogin(_dbContext);
            DataTestHelper.SeedClaims(_dbContext);
        }


        [Fact]
        public void GetListCLaims_Should_Get_CLaimsList()
        {
            Assert.NotEmpty(_repository.GetList());
        }


        [Fact]
        public void Can_UpdateFSAClaim()
        {

            string refNo = Guid.NewGuid().ToString();
            var claim = _dbContext.FSAClaims.First();
            claim.ReferenceNumber = refNo;
            _repository.Update(claim, x => x.ID == claim.ID);
            Assert.Equal(refNo, _dbContext.FSAClaims.SingleOrDefault(x => x.ID == claim.ID).ReferenceNumber);
        }


        [Fact]
        public void Can_AddFSAClaim()
        {
            var claim = DataTestHelper.GenerateClaim();
            claim.EmployeeID = _employeeID;
            var result = _repository.Add(claim);
            Assert.True(result.IsSuccess);
            _claimID = claim.ID;
            //Assert.Equal(EntityState.Added, _dbContext.Entry(claim).State);
        }

        [Fact]
        public void Can_DeleteClaim()
        {
            var claim = _dbContext.FSAClaims.First();
            var result = _repository.Delete(true, c => c.ID == claim.ID);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Can_GetClaim()
        {
            int id = _dbContext.FSAClaims.First().ID;
            var claim = _repository.Get(x => x.ID == id);
            Assert.NotNull(claim);
        }
    }
}
