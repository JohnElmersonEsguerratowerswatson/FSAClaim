﻿using FSA.Data.DBContext;
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

                    int rows = _dbContext.SaveChanges();
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
                using (_dbContext)
                {

                    T? entity = _dbContext.Set<T>().Where(predicate).SingleOrDefault();
                    if (entity == null) return new ClaimRepositoryResult(false, "Not Found", "");
                    _dbContext.Remove<T>(entity);
                    int rows = _dbContext.SaveChanges();
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

        public T Get(Func<T, bool> criteria)
        {
            try
            {
                using (_dbContext)
                {

                    T? entity = _dbContext.Set<T>().AsQueryable().SingleOrDefault<T>(criteria);
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
                using (_dbContext)
                {
                    var claim = _dbContext.Set<T>().SingleOrDefault<T>(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");
                    claim = entity;

                    int rows = _dbContext.SaveChanges();
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }

        }


    }
}
