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
    public class TransactAssociateEntityRepository : ITransactAssociateEntityRepository<Employee, EmployeeFSA, FSARule>
    {
        private FSAClaimContext _dbContext;

        public TransactAssociateEntityRepository(FSAClaimContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepositoryResult Add(FSARule entity, int innerID)
        {

            try
            {
                if (entity.ID == 0)
                {
                    _dbContext.FSARules.Add(entity);
                    _dbContext.SaveChanges();
                }

                var employeeFSA = new EmployeeFSA { EmployeeID = innerID, FSAID = entity.ID };
                _dbContext.Add(employeeFSA);
                _dbContext.SaveChanges();
                //
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

