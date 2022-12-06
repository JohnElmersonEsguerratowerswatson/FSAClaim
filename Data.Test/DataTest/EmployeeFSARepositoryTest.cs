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
    public class EmployeeFSARepositoryTest
    {
        private EmployeeFSARepository _repository;
        private FSAClaimContext _dbContext;

        public EmployeeFSARepositoryTest()
        {
            DbContextOptionsBuilder<FSAClaimContext> optionsBuilder = new DbContextOptionsBuilder<FSAClaimContext>();
            optionsBuilder.UseInMemoryDatabase<FSAClaimContext>("FSAClaimsTestDB");
            _dbContext = new FSAClaimContext(optionsBuilder.Options);
            _repository = new EmployeeFSARepository(_dbContext);

            Setup();
        }

        private void Setup()
        {
            DataTestHelper.SeedEmployees(_dbContext);
            DataTestHelper.SeedFSARules(_dbContext);
            DataTestHelper.SeedEmployeeFSAs(_dbContext);
        }

        [Fact]
        public void GetRule_Should_Return_Rule()
        {
            var employee = _dbContext.Employees.First();
            var result = _repository.Get(e => e.ID == employee.ID);

            var rule = _dbContext.FSARules.Join(_dbContext.EmployeeFSAs.Where(ef => ef.EmployeeID == employee.ID), r => r.ID, ef => ef.FSAID,
            (ir, ief) => new FSARule
            {
                FSALimit = ir.FSALimit,
                YearCoverage = ir.YearCoverage,
                ID = ir.ID
            }).First();
            
            Assert.NotNull(result);
            Assert.Equal(result.ID, rule.ID);
            Assert.Equal(result.FSALimit,rule.FSALimit);
            Assert.Equal(result.YearCoverage, rule.YearCoverage);
        }
    }
}
