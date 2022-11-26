using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class FSARuleLogic
    {
        private int _employeeID;
        public FSARuleLogic(int employeeID)
        {
            _employeeID = employeeID;

        }

        private Employee GetEmployee()
        {
            TRepository<Employee> repoE = new TRepository<Employee>();
            var employee = repoE.Get(e => e.ID == _employeeID);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");
            return employee;
        }

        private FSARule GetFSARule()
        {
            EmployeeFSARepository repoeFSA = new EmployeeFSARepository();
            var fsaRule = repoeFSA.Get(e => e.ID == _employeeID);
            return fsaRule;
        }

        public TransactFSARule Get()
        {
            var employee = GetEmployee();
            var fsaRule = GetFSARule();
            if (fsaRule == null) fsaRule = new FSARule { FSALimit = 0, ID = _employeeID, YearCoverage = DateTime.UtcNow.Year };
            return new TransactFSARule
            {
                FSAAmount = fsaRule.FSALimit,
                EmployeeID = _employeeID,
                EmployeeName = employee.FirstName + " " + employee.LastName,
                YearCoverage = fsaRule.YearCoverage
            };
        }



        public IAddFSARuleResult AddFSARule(ITransactFSARule getFSA)
        {
            try
            {
                // TRepository<FSARule> repository  = new TRepository<FSARule>();
                TransactAssociateEntityRepository repo = new TransactAssociateEntityRepository();
                var repoResult = repo.Add(
                    new FSARule { FSALimit = getFSA.FSAAmount, YearCoverage = getFSA.YearCoverage }, _employeeID
                    );
                return new AddFSARuleResult { IsSuccess = repoResult.IsSuccess };//  repository.Add();
            }
            catch
            {
                return new AddFSARuleResult { IsSuccess = false, Message = "There was a problem Adding FSA rule" };
            }
        }
    }
}
