using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using FSA.Common.Enums;
using FSA.Common;

namespace FSA.API.Business
{
    public class ClaimsBusinessLogic : IFSAClaimBusiness
    {

        private int _employeeNumber;

        public ClaimsBusinessLogic(int employeeNumber)
        {
            _employeeNumber = employeeNumber;
        }

        public decimal ComputeRemainingFSA()
        {
            FSAClaimRepository repository = new FSAClaimRepository();

            //get Pending and Approved FSAClaims of employee for the year
            var employeeClaims = repository.GetList(ec => ec.EmployeeID == _employeeNumber && (ec.Status == "Approved" || ec.Status == "Pending") && ec.ApprovalDate.Year == DateTime.Now.Year);
            //Compute Total Approved Claims for the year
            decimal totalClaims = employeeClaims.Sum(ec => ec.ClaimAmount);
            


            //employee fsa rule to compare limit to existing
            EmployeeFSARepository eFSARepository = new EmployeeFSARepository();

            var employeeFSARule = eFSARepository.Get(e => e.ID == _employeeNumber);

            if (employeeFSARule == null) throw new KeyNotFoundException();
            //Compute Remaining FSA
            decimal remainingFSA = employeeFSARule.FSALimit - totalClaims;
            return remainingFSA;
        }

        public IClaimResult AddClaim(IViewClaim claim)
        {
            ClaimApproval approval = ClaimApproval.Pending;

            var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);
            
            //Check if Receipt Date is valid
            if (claimReceiptDate.Year != DateTime.UtcNow.Year) approval = ClaimApproval.Denied; //return new ClaimResult { IsSuccess = false, Message = "BadRequest" };
            
            //Compose Reference Number
            string refNo = claimReceiptDate.ToString("yyy") + claimReceiptDate.Month.ToString("MM") + claim.ReceiptNumber.ToString();


            FSAClaimRepository repository = new FSAClaimRepository();


            //Compute Remaining FSA
            decimal remainingFSA = 0;
            try { remainingFSA = ComputeRemainingFSA(); }
            catch { return new ClaimResult { IsSuccess = false, Message = ObjectStatus.ObjectNotFound }; }

            //Check if FSA amount is greater than Remaining FSA
            if (remainingFSA < claim.ClaimAmount) return new ClaimResult { IsSuccess = false, Message = "You only have " + remainingFSA + "." };

            try
            {
                var claimAdd = CreateClaim(claim.ClaimAmount, claim.ReceiptAmount, claimReceiptDate, claim.ReceiptNumber, approval, int.Parse(refNo), _employeeNumber);
                var result = repository.Add(claimAdd);

                return new ClaimResult { IsSuccess = true, Message = "Submitted for approval." };
            }
            catch { return new ClaimResult { IsSuccess = false, Message = "Server Error: Please file a ticket" }; }

        }

        private FSAClaim CreateClaim(decimal claimAmount, decimal receiptAmount, DateTime receiptDate, string receiptNumber, ClaimApproval claimApproval, int refNo, int employeeId)
        {
            return new FSAClaim
            {
                ApprovalDate = DateTime.UtcNow,
                ClaimAmount = claimAmount,
                DateSubmitted = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                ReceiptAmount = receiptAmount,
                ReceiptDate = receiptDate,
                ReceiptNumber = receiptNumber,
                Status = claimApproval.ToString(),
                EmployeeID = employeeId,//get from db or User .Net variable
                ReferenceNumber = refNo// generate a unique number
            };
        }

        public IClaimResult Delete(ITransactClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);
            var repositoryResult = repository.Delete(c => c.ReceiptDate == claimReceiptDate && c.ReceiptNumber == claim.ReceiptNumber);//ew FSAClaim { ReceiptDate = claim.ReceiptDate, ReceiptAmount = claim.ReceiptAmount}
            if (!repositoryResult.IsSuccess) return new ClaimResult { IsSuccess = false, Message = repositoryResult.Error.Message };
            return new ClaimResult { IsSuccess = true };
        }

        public IViewClaim GetClaim(int referenceNumber)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claim = repository.Get(c => c.ReferenceNumber == referenceNumber);
            //var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);
            if (claim == null) return null;
            return new EmployeeClaim { ClaimAmount = claim.ClaimAmount, ReceiptAmount = claim.ReceiptAmount, ReceiptDate = claim.ReceiptDate.ToString("MM/dd/yyyy"), ReceiptNumber = claim.ReceiptNumber, DateSubmitted = claim.DateSubmitted.ToString("MM/dd/yyyy"), ReferenceNumber = claim.ReferenceNumber.ToString(), Status = claim.Status, TotalClaimAmount = claim.ClaimAmount };
        }

        public List<ClaimsTableItem> GetClaimList()
        {

            FSAClaimRepository repository = new FSAClaimRepository();
            var claimList = repository.GetList(c => c.EmployeeID == _employeeNumber);
            if (claimList == null || claimList.Count() == 0) return new List<ClaimsTableItem>();
            List<ClaimsTableItem> claimItems = new List<ClaimsTableItem>();
            foreach (var claim in claimList)
            {
                claimItems.Add(
                    new ClaimsTableItem
                    {
                        ClaimAmount = claim.ClaimAmount,
                        ReceiptAmount = claim.ReceiptAmount,
                        ReceiptDate = claim.ReceiptDate.ToString("MM/dd/yyyy"),
                        ReceiptNumber = claim.ReceiptNumber,
                        ReferenceNumber = claim.ReferenceNumber.ToString(),
                        DateSubmitted = claim.DateSubmitted.ToString("MM/dd/yyyy"),
                        TotalClaimAmount = claim.ClaimAmount,
                        Status = claim.Status
                    }
                    );
            }
            return claimItems;
        }


        public IClaimResult Update(ITransactClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);

            var dbClaim = new FSAClaim
            {
                DateSubmitted = DateTime.UtcNow,
                ReceiptDate = claimReceiptDate,
                ReceiptAmount = claim.ReceiptAmount,
                ReceiptNumber = claim.ReceiptNumber,
                ClaimAmount = claim.ClaimAmount
            };

            var result = repository.Update(dbClaim, c => c.ReferenceNumber.ToString() == claim.ReferenceNumber);
            return new ClaimResult { IsSuccess = result.IsSuccess };
        }

    }
}
