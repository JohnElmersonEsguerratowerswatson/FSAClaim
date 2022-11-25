using FSA.Data.DBContext;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.FSARuleRepository
{
    public class TransactAssociateEntityRerpository : ITransactAssociateEntityRepository<Employee, EmployeeFSA, FSARule>
    {
        private FSAClaimContext _dbContext;

        public TransactAssociateEntityRerpository()
        {
            _dbContext = new FSAClaimContext();
        }
        public IRepositoryResult Add(FSARule entity, int innerID)
        {
            try
            {
                _dbContext.FSARules.Add(entity);
                
                var employeeFSA = new EmployeeFSA { EmployeeID = innerID, FSAID = entity.ID };
                _dbContext.Add(employeeFSA);
                _dbContext.SaveChanges();
                //_dbContext.SaveChanges();
                var result = new ClaimRepositoryResult(true);
                return result;
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message);
            }
        }

        public FSARule? Get(Func<Employee, bool> criteria)
        {
            return null;
        }
    }

}

