using FSA.Data.DBContext;
using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.FSAClaimRepository
{
    public class EmployeeFSARepository : IJoinRepository<Employee, EmployeeFSA, FSARule>
    {
        private FSAClaimContext _dbContext;

        public EmployeeFSARepository(FSAClaimContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FSARule? Get(Func<Employee, bool> criteria)
        {
            //select the employee with the supplied ID etc.
            Employee? employee = _dbContext.Employees.Where(criteria).SingleOrDefault();
            //return null if Employee not found
            if (employee == null) return null;
            //get EmployeeFSA where employee ID 
            IEnumerable<EmployeeFSA> employeeFSAs = _dbContext.EmployeeFSAs.Where(ef => ef.EmployeeID == employee.ID);
            //return null if no EmployeeFSA found
            if (employeeFSAs.Count() <= 0) return null;
            //initialize nullable FSARule return variable
            FSARule? fSARule = null;
            var rule = _dbContext.FSARules.Join(_dbContext.EmployeeFSAs.Where(ef => ef.EmployeeID == employee.ID), r => r.ID, ef => ef.FSAID,
            (ir, ief) => new FSARule
            {
                FSALimit = ir.FSALimit,
                YearCoverage = ir.YearCoverage,
                ID = ir.ID
            });
            return rule.SingleOrDefault();
        }


        ///Not Implemented
        ICollection<FSARule> IJoinRepository<Employee, EmployeeFSA, FSARule>.GetList(Func<Employee, bool> criteria)
        {
            throw new NotImplementedException();
        }
    }
}
