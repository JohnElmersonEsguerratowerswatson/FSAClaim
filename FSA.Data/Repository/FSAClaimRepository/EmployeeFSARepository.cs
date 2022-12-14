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

        public EmployeeFSARepository()
        {
            _dbContext = new FSAClaimContext();
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

            //loop thru each EmployeeFSA then query dbContext for FSA where EmployeeFSA Employee.ID. T
            ////inside loop query FSARules for EmployeeFSA.FSAID AND Year Coverage is equal to this year
            //select one and return
            //employeeFSAs.AsQueryable().ForEachAsync(eF =>
            //{
            //    if (eF.EmployeeID != employee.ID) return;
            //    fSARule = _dbContext.FSARules.SingleOrDefault(fR => fR.ID == eF.FSAID && fR.YearCoverage == DateTime.UtcNow.Year);
            //    if (fSARule != null) return;

            //});

            //        var Track = db.Track
            //.Join(db.MediaType,
            //    o => o.MediaTypeId,
            //    i => i.MediaTypeId,
            //    (o, i) =>
            //    new
            //    {
            //        Name = o.Name,
            //        Composer = o.Composer,
            //        MediaType = i.Name
            //    }
            //)
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
