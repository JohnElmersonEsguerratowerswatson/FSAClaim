using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface IRepository<T>:IViewRepository<T>,ITransactRepository<T>  where T : class
    {
    }
}
