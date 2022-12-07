using AutoFixture;
using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
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
    public class TransactAssociateEntityRepositoryTest
    {
        private TransactAssociateEntityRepository _repository;
        private FSAClaimContext _dbContext;

        public TransactAssociateEntityRepositoryTest()
        {
            DbContextOptionsBuilder<FSAClaimContext> optionsBuilder = new DbContextOptionsBuilder<FSAClaimContext>();
            optionsBuilder.UseInMemoryDatabase<FSAClaimContext>("FSAClaimsTestDB");
            _dbContext = new FSAClaimContext(optionsBuilder.Options);
            _repository = new TransactAssociateEntityRepository(_dbContext);
            Setup();
        }

        private void Setup()
        {
           
            DataTestHelper.SeedEmployees(_dbContext);
            DataTestHelper.SeedFSARules(_dbContext);
            // DataTestHelper.SeedEmployeeFSAs(_dbContext);
           // _dbContext.EmployeeFSAs.RemoveRange(_dbContext.EmployeeFSAs);
        }


        [Fact]
        public void Add_Should_Return_Success()
        {
            FSARule rule = new Fixture().Create<FSARule>();
            rule.ID = 0;
            int id = _dbContext.Employees.First().ID;
            var result = _repository.Add(rule, id);
            Assert.True(result.IsSuccess);
        }
    }
}
