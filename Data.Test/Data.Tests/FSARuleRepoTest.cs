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
        private TransactAssociateEntityRepository repository;
        private int _employeeID;//Change value accdgly
        public FSARuleRepoTest()
        {
            repository = new TransactAssociateEntityRepository();
            _employeeID = 3;
        }

        [Fact]
        public void Add()
        {
            var result = repository.Add(new Domain.Entities.FSARule
            {
                FSALimit = 5000,
                YearCoverage = 2022
            }, _employeeID);

            Assert.True(result.IsSuccess);
        }
    }
}