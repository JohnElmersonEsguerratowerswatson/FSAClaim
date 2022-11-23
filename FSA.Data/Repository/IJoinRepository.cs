using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    internal interface IJoinRepository<T, U, V> where T : class where U : class where V : class

    {
        public ICollection<V> GetList(Func<T, bool> criteria);
        public V? Get (Func<T, bool> criteria);
    }
}
