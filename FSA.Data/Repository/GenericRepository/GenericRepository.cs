using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.GenericRepository
{
    public class GenericRepository<T> : IViewRepository<T>, ITransactRepository<T> where T : class
    {
        private FSAClaimContext _dbContext;

        public GenericRepository()
        {
            _dbContext = new FSAClaimContext();
        }
        public IRepositoryResult Add(T entity)
        {
            try
            {
                using (_dbContext)
                {
                    _dbContext.Add(entity);
                }
                return new ClaimRepositoryResult(true);
            }
            catch (Exception ex)
            {

                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        public IRepositoryResult Delete(T entity)
        {
            try
            {
                using (_dbContext)
                {

                    _dbContext.Remove(entity);
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
                using (_dbContext)
                {

                    var list = _dbContext.Set<T>().AsQueryable().Where<T>(criteria);
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
                using (_dbContext)
                {

                    var list = _dbContext.Set<T>().AsQueryable();
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public IRepositoryResult SaveChanges()
        {
            try
            {
                using (_dbContext)
                {

                    int currentRows = _dbContext.SaveChanges();
                    return new ClaimRepositoryResult(true, rows: currentRows);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        public IRepositoryResult Update(T entity, Func<T, bool> predicate)
        {
            try
            {
                using (_dbContext)
                {
                    var claim = _dbContext.Set<T>().Where<T>(predicate).SingleOrDefault();
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");
                    claim = entity;
                }
                return new ClaimRepositoryResult(true);
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }

        }
    }
}
