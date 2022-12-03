using FSA.Data.DBContext;
using FSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.GenericRepository
{
    public class TRepository<T> : GenericRepository<T> where T : class
    {
        protected override FSAClaimContext ClaimContext { get; set; }
        public TRepository()
        {
            ClaimContext = new FSAClaimContext();
        }

        public override IRepositoryResult Update(string status, Func<FSAClaim, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
