using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.GenericRepository
{
    public abstract class GenericRepository<T> : IViewRepository<T>, ITransactRepository<T> where T : class
    {
        protected abstract  FSAClaimContext ClaimContext { get; set; }
        protected bool _test = false;//FOR Unit Testing

        public IRepositoryResult Add(T entity)
        {
            try
            {
                using (ClaimContext)
                {
                    ClaimContext.Add(entity);

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {

                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        public IRepositoryResult Delete(Func<T, bool> predicate)
        {
            try
            {
                using (ClaimContext)
                {

                    T? entity = ClaimContext.Set<T>().Where(predicate).SingleOrDefault();
                    if (entity == null) return new ClaimRepositoryResult(false, "Not Found", "");
                    ClaimContext.Remove<T>(entity);
                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        public ICollection<T> GetList(Func<T, bool> criteria)
        {
            try
            {
                using (ClaimContext)
                {

                    var list = ClaimContext.Set<T>().AsQueryable().Where<T>(criteria);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public ICollection<T> GetList()
        {
            try
            {
                using (ClaimContext)
                {

                    var list = ClaimContext.Set<T>().AsQueryable();
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public T Get(Func<T, bool> criteria)
        {
            try
            {
                using (ClaimContext)
                {

                    T? entity = ClaimContext.Set<T>().AsQueryable().SingleOrDefault<T>(criteria);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IRepositoryResult Update(T entity, Func<T, bool> predicate)
        {
            try
            {
                using (ClaimContext)
                {
                    var claim = ClaimContext.Set<T>().SingleOrDefault<T>(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");
                    claim = entity;

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }

        }

        

        protected int Save(FSAClaimContext dbContext)
        {
            if (_test) return 1;//return success do not save changes
            int rows = dbContext.SaveChanges();
            return rows;
        }
    }
}
