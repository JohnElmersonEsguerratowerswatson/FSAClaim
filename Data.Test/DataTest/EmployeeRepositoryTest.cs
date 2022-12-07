using AutoFixture;
using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
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
    public class EmployeeRepositoryTest
    {

        private TRepository<Employee> _repository;
        private FSAClaimContext _dbContext;
        private DbContextOptionsBuilder<FSAClaimContext> _optionsBuilder;
        private Fixture _fixture;


        public EmployeeRepositoryTest()
        {
            _optionsBuilder = new DbContextOptionsBuilder<FSAClaimContext>();
            _optionsBuilder.UseInMemoryDatabase<FSAClaimContext>("FSAClaimsTestDB");
            _dbContext = new FSAClaimContext(_optionsBuilder.Options);
            _repository = new TRepository<Employee>(_dbContext);
            _fixture = new Fixture();
            Setup();
        }

        private void Setup()
        {
            
            DataTestHelper.SeedEmployees(_dbContext);
           
        }

        [Fact]
        public void GetList_Should_Return_EmployeeList()
        {
              Assert.NotEmpty(_repository.GetList());
        }

        [Fact]
        public void AddEmployee_Should_Return_SuccessResult()
        {
            var employee = _fixture.Create<Employee>();
            employee.ID = 0;
            var result = _repository.Add(employee);
            Assert.True(result.IsSuccess);
        }

    }
}
