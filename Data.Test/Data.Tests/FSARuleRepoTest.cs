using FSA.Data.Repository.FSARuleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.Data.Tests
{
    public class FSARuleRepoTest
    {
        private TransactAssociateEntityRerpository repository;
        private int _employeeID;
        public FSARuleRepoTest()
        {
            repository = new TransactAssociateEntityRerpository();
          
        }
        [Fact]
        public void Add()
        {
            var result = repository.Add(new Domain.Entities.FSARule
            {
                FSALimit = 5000,
                YearCoverage = 2024
            }, _employeeID);

            Assert.True(result.IsSuccess);
        }
    }
}
