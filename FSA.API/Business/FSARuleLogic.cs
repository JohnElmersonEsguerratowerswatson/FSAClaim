using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class FSARuleLogic
    {
        private Employee _employee;
        private int _employeeID;
        private string _employeeName;
        public FSARuleLogic(int employeeID)
        {

            _employeeID = employeeID;
            _employee = GetEmployee();
            _employeeName = _employee.FirstName + " " + _employee.LastName;
        }

        private bool ValidateEmployeeName(Employee employee)
        {
            bool qualifiedByFirsNameLastName = false;//FirstName LastName
            bool qualifiedByWholeName = false;//FirstName MiddleName LastName
            bool qualifiedByFullName = false; //FirstName MI. LastName

            qualifiedByFirsNameLastName = employee.FirstName + " " + employee.LastName == _employeeName;
            qualifiedByWholeName = employee.FirstName + " " + employee.MiddleName.Substring(0, 1) + ". " + employee.LastName == _employeeName;
            qualifiedByFullName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName == _employeeName;

            return qualifiedByFirsNameLastName || qualifiedByFullName || qualifiedByWholeName;
        }

        private Employee GetEmployee()
        {
            TRepository<Employee> repoE = new TRepository<Employee>();
            var employee = repoE.Get(e => e.ID == _employeeID);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");
            return employee;
        }

        private FSARule GetEmployeeFSARule()
        {
            EmployeeFSARepository repoeFSA = new EmployeeFSARepository();
            var fsaRule = repoeFSA.Get(e => e.ID == _employee.ID);
            return fsaRule;
        }

        private FSARule GetFSARuleFromYearAndAmount(int year, decimal amount)
        {
            TRepository<FSARule> repository = new TRepository<FSARule>();
            var rule = repository.Get(r => r.FSALimit == amount && r.YearCoverage == year);
            return rule;
        }

        public TransactFSARule Get()
        {
            var employee = GetEmployee();
            var fsaRule = GetEmployeeFSARule();
            if (fsaRule == null) return null;
            return new TransactFSARule
            {
                FSAAmount = fsaRule.FSALimit,
                //EmployeeName = employee.FirstName + " " + employee.LastName,
                YearCoverage = fsaRule.YearCoverage
            };
        }

        public IAddFSARuleResult AddFSARule(ITransactFSARule getFSA)
        {
            try
            {
                // TRepository<FSARule> repository  = new TRepository<FSARule>();
                IRepositoryResult repoResult;
                TransactAssociateEntityRepository repo = new TransactAssociateEntityRepository();
                //check for existing Rule with same Year and Amount
                FSARule rule = GetFSARuleFromYearAndAmount(getFSA.YearCoverage, getFSA.FSAAmount);
                //Add new Rule
                if (rule == null) { rule = new FSARule { FSALimit = getFSA.FSAAmount, YearCoverage = getFSA.YearCoverage }; }
                repoResult = repo.Add(
                   rule, _employee.ID
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
