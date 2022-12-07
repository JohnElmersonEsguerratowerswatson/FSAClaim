using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Common;
using FSA.Common.Enums;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class ClaimsApprovalLogic : IClaimsApprovalService
    {
        private IRepository<Employee> _employeeRepository;
        private IRepository<FSAClaim> _fsaClaimRepository;

        public ClaimsApprovalLogic(IRepository<Employee> employeeRepository, IRepository<FSAClaim> fsaClaimRepository)
        {
            _fsaClaimRepository = fsaClaimRepository;
            _employeeRepository = employeeRepository;
        }

        public List<ClaimsApprovalTableItems> GetTableView()
        {

            List<ClaimsApprovalTableItems> items = new List<ClaimsApprovalTableItems>();
            var employees = _employeeRepository.GetList();
            foreach (var employee in employees)
            {
                var claims = _fsaClaimRepository.GetList(c => c.isCancelled == false && c.EmployeeID == employee.ID && c.Status == ClaimApprovals.Pending.ToString() && c.DateSubmitted.Year == DateTime.UtcNow.Year);
                foreach (var claim in claims)
                    items.Add(new ClaimsApprovalTableItems
                    {
                        Status = claim.Status,
                        ClaimAmount = claim.ClaimAmount,
                        ReceiptAmount = claim.ReceiptAmount,
                        TotalClaimAmount = claim.ClaimAmount,
                        DateSubmitted = claim.DateSubmitted.ToString("MM/dd/yyyy"),
                        ReceiptDate = claim.ReceiptDate.ToString("MM/dd/yyyy"),
                        ReceiptNumber = claim.ReceiptNumber,
                        ReferenceNumber = claim.ReferenceNumber,
                        EmployeeID = employee.ID,
                        EmployeeName = employee.FirstName + " " + employee.LastName
                    });
            }
            return items;
        }

        public bool ApproveClaim(ClaimApproval claimApproval)
        {
            string approve = claimApproval.Approve ? ClaimApprovals.Approved.ToString() : ClaimApprovals.Denied.ToString();
            var result = _fsaClaimRepository.Update(approve, claim => claim.ReferenceNumber == claimApproval.ReferenceNumber);

            return result.IsSuccess;
        }
    }
}