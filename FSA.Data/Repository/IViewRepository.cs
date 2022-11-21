using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface IViewRepository<T> where T : class
    {
        public ICollection<T> GetList(Func<T, bool> criteria);

        //public IQueryable<T> GetList(Func<T, bool> criteria, IEntity parent );
        public ICollection<T> GetList();
    }
}
