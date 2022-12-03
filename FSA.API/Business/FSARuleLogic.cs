using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class FSARuleLogic : IFSARuleService
    {
        private Employee _employee;
        private int _employeeID;
        private string _employeeName;
        private IRepository<Employee> _employeeRepository;
        private IJoinRepository<Employee, EmployeeFSA, FSARule> _employeeFSARepository;
        private IRepository<FSARule> _fsaRuleRepository;

        public int EmployeeID { get { return _employeeID; } set { SetEmployee(value); } }

        public FSARuleLogic(IRepository<Employee> employeeRepository, IJoinRepository<Employee, EmployeeFSA, FSARule> employeeFSARepository, IRepository<FSARule> fsaRuleRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeFSARepository = employeeFSARepository; 
            _fsaRuleRepository = fsaRuleRepository; 
        }

        private void SetEmployee(int employeeID)
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

            var employee = _employeeRepository.Get(e => e.ID == _employeeID);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");
            return employee;
        }

        private FSARule GetEmployeeFSARule()
        {
            
            var fsaRule = _employeeFSARepository.Get(e => e.ID == _employee.ID);
            return fsaRule;
        }

        private FSARule GetFSARuleFromYearAndAmount(int year, decimal amount)
        {
          
            var rule = _fsaRuleRepository.Get(r => r.FSALimit == amount && r.YearCoverage == year);
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
                TransactFSARule rule;
                try
                {
                    rule = Get();
                }
                catch (KeyNotFoundException ex)
                {
                    return new AddFSARuleResult { IsSuccess = false, Message = "Unable to find employee." };
                }
                catch (Exception)
                {
                    return new AddFSARuleResult { IsSuccess = false, Message = "There was a problem adding FSA." };
                }


                if (rule != null)
                {

                    return new AddFSARuleResult { IsSuccess = false, Message = "Employee already has FSA." };
                }
                // TRepository<FSARule> repository  = new TRepository<FSARule>();
                IRepositoryResult repoResult;
                TransactAssociateEntityRepository repo = new TransactAssociateEntityRepository();
                //check for existing Rule with same Year and Amount
                FSARule dbRule = GetFSARuleFromYearAndAmount(getFSA.YearCoverage, getFSA.FSAAmount);
                //Add new Rule

                if (dbRule == null) { dbRule = new FSARule { FSALimit = getFSA.FSAAmount, YearCoverage = getFSA.YearCoverage }; }
                repoResult = repo.Add(
                   dbRule, _employee.ID
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
