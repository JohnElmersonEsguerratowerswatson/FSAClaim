using FSA.Data.DBContext;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.LoginRepository
{
    public class LoginRepository :IViewRepository<Login>
    {

        private FSAClaimContext _dbContext;

        public LoginRepository()
        {
            _dbContext = new FSAClaimContext();
        }

        public Login Get(Func<Login, bool> predicate)
        {
            return _dbContext.Logins.SingleOrDefault(predicate);
        }


        #region IViewRepository NOT IMPLEMENTED
        //NOT IMPLEMENTED
        public ICollection<Login> GetList(Func<Login, bool> criteria)
        {
            throw new NotImplementedException();
        }
        //NOT IMPLEMENTED
        public ICollection<Login> GetList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
