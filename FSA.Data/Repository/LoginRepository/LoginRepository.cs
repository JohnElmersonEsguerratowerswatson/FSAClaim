﻿using FSA.Data.DBContext;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.LoginRepository
{
    public class LoginRepository : GenericRepository<Login>
    {

        private FSAClaimContext _dbContext;

        public LoginRepository()
        {
            _dbContext = new FSAClaimContext();
        }

        public new Login Get(Func<Login, bool> predicate)
        {
            try
            {
                using (_dbContext)
                {      
                    return _dbContext.Logins.SingleOrDefault(predicate);
                }
            }
            catch
            {
                return null;
            }
        }

        public new ICollection<Login>  GetList(Func<Login, bool> criteria)
        {
            try
            {
                using (_dbContext)
                {

                    return _dbContext.Logins.Where(criteria).ToList();
                }
            }
            catch
            {
                return new List<Login>();
            }
        }

        public new ICollection<Login> GetList()
        {
            try
            {
                using (_dbContext)
                {

                    return _dbContext.Logins.ToList();
                }
            }
            catch
            {
                return new List<Login>();
            }
        }
    }
}