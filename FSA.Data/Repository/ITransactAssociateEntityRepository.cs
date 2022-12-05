using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface ITransactAssociateEntityRepository<Inner,Join,Outer> where Inner : class where Outer : class where Join : class
    {
        public IRepositoryResult Add(Outer entity, int innerID);
    }
}
