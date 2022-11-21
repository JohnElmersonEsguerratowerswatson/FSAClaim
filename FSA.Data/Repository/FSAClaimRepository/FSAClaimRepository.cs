using FSA.Data.Repository;
using FSA.Domain.Entities;
using FSA.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.FSAClaimRepository
{

    public class FSAClaimRepository : IViewRepository<FSAClaim>, ITransactRepository<FSAClaim>
    {

        private FSAClaimContext _dbContext;

        public FSAClaimRepository()
        {
            _dbContext = new FSAClaimContext();
        }
        public IRepositoryResult Add(FSAClaim entity)
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

        public IRepositoryResult Delete(FSAClaim entity)
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

        public ICollection<FSAClaim> GetList(Func<FSAClaim, bool> criteria)
        {
            try
            {
                using (_dbContext)
                {

                    return _dbContext.FSAClaims.Where(criteria).ToList();
                }
            }
            catch
            {
                return new List<FSAClaim>();
            }
        }

        public ICollection<FSAClaim> GetList()
        {
            try
            {
                using (_dbContext)
                {

                    return _dbContext.FSAClaims.ToList();
                }
            }
            catch
            {
                return new List<FSAClaim>();
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

        public IRepositoryResult Update(FSAClaim entity, Func<FSAClaim, bool> predicate)
        {
            try
            {
                using (_dbContext)
                {
                    var claim = _dbContext.FSAClaims.Where(predicate).SingleOrDefault();
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
