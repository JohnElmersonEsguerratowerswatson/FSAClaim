using FSA.Data.DBContext;
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
    }
}
