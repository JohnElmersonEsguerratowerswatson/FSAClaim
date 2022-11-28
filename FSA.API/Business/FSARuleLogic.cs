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
        private string _employeeName;
        public FSARuleLogic(int employeeID, string employeeName)
        {
            _employeeID = employeeID;
            _employeeName = employeeName.Trim();
        }

        private bool ValidateEmployeeNameAndID(Employee employee)
        {

            bool qualifiedByFirsNameLastName = false;//FirstName LastName
            bool qualifiedByWholeName = false;//FirstName MiddleName LastName
            bool qualifiedByFullName = false; //FirstName MI. LastName
            bool qualifiedByID = false;//employeeID

            qualifiedByID = employee.ID == _employeeID;
            if (!qualifiedByID) return false;

            qualifiedByFirsNameLastName = employee.FirstName + " " + employee.LastName == _employeeName;
            qualifiedByWholeName = employee.FirstName + " " + employee.MiddleName.Substring(0, 1) + ". " + employee.LastName == _employeeName;
            qualifiedByFullName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName == _employeeName;

            return qualifiedByFirsNameLastName || qualifiedByFullName || qualifiedByWholeName;
        }

        private Employee GetEmployee()
        {
            TRepository<Employee> repoE = new TRepository<Employee>();
            var employee = repoE.Get(e => ValidateEmployeeNameAndID(e));
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
            // if (fsaRule == null) fsaRule = new FSARule { FSALimit = 0, ID = _employeeID, YearCoverage = DateTime.UtcNow.Year };
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
                var rule = GetFSARule();

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
