using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Common.Enums;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class ClaimsApprovalLogic
    {
        public List<ClaimsApprovalTableItems> GetTableView()
        {
            TRepository<Employee> employeeRepository = new TRepository<Employee>();
            TRepository<FSAClaim> claimRepositorys = new TRepository<FSAClaim>();
            List<ClaimsApprovalTableItems> items = new List<ClaimsApprovalTableItems>();
            var employees = employeeRepository.GetList(e => true);
            foreach (var employee in employees)
            {
                var claims = claimRepositorys.GetList(c => c.EmployeeID == employee.ID && c.Status == ClaimApprovals.Pending.ToString() && c.DateSubmitted.Year == DateTime.UtcNow.Year);
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
            FSAClaimRepository claimsRepository = new FSAClaimRepository();
            var result = claimsRepository.Update(claimApproval.Approve, claim => claim.ReferenceNumber == claimApproval.ReferenceNumber);

            return result.IsSuccess;
        }
    }
}