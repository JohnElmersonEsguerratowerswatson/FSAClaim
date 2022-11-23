using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface ITransactRepository<T> where T : class
    {


        public IRepositoryResult Update(T entity, Func<T, bool> predicate);
        public IRepositoryResult Delete(Func<T, bool> predicate);
        public IRepositoryResult Add(T entity);

    }
}
