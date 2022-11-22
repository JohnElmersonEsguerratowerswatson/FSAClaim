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

        public IClaimResult AddClaim(IClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            TRepository<FSARule> tRepo = new TRepository<FSARule>();

            ClaimApproval approval = ClaimApproval.Pending;

            if (claim == null) return new ClaimResult { IsSuccess = false, Message = "BadRequest" };
            if (claim.ReceiptDate.Year != DateTime.UtcNow.Year) approval = ClaimApproval.Denied; //return new ClaimResult { IsSuccess = false, Message = "BadRequest" };

            var employeeClaims = repository.GetList(ec => ec.EmployeeID == _employeeNumber && ec.Status == "Approved" && ec.ApprovalDate.Year == DateTime.Now.Year);
            decimal totalClaims = employeeClaims.Sum(ec => ec.ClaimAmount);
            string refNo = claim.ReceiptDate.Year.ToString("yyy") + claim.ReceiptDate.Month.ToString("MM") + claim.ReceiptNumber.ToString();
            //employee fsa rule to compare limit to existing
            EmployeeFSARepository eFSARepository = new EmployeeFSARepository();
            var employeeFSARule = eFSARepository.Get(e => e.ID == _employeeNumber);
            if (employeeFSARule == null) return new ClaimResult { IsSuccess = false, Message = "FSA NotFound" };
            decimal remainingFSA = employeeFSARule.FSALimit - totalClaims;

            if (remainingFSA < claim.ClaimAmount) return new ClaimResult { IsSuccess = false, Message = "You only have " + remainingFSA + "." };


            try
            {
                var claimAdd = CreateClaim(claim.ClaimAmount, claim.ReceiptAmount, claim.ReceiptDate, claim.ReceiptNumber, approval, int.Parse(refNo), _employeeNumber);
                var result = repository.Add(claimAdd);

                return new ClaimResult { IsSuccess = true, Message = "" };
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

        public IClaimResult Delete(IClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var repositoryResult = repository.Delete(c => c.ReceiptDate == claim.ReceiptDate && c.ReceiptNumber == claim.ReceiptNumber);//ew FSAClaim { ReceiptDate = claim.ReceiptDate, ReceiptAmount = claim.ReceiptAmount}
            if (!repositoryResult.IsSuccess) return new ClaimResult { IsSuccess = false, Message = repositoryResult.Error.Message };
            return new ClaimResult { IsSuccess = true };
        }

        public IClaim GetClaim(int referenceNumber)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claim = repository.Get(c => c.ReferenceNumber == referenceNumber);
            if (claim == null) return null;
            return new EmployeeClaim { ClaimAmount = claim.ClaimAmount, ReceiptAmount = claim.ReceiptAmount, ReceiptDate = claim.ReceiptDate, ReceiptNumber = claim.ReceiptNumber };
        }

        public List<IClaimTableItem> GetClaimList()
        {

            FSAClaimRepository repository = new FSAClaimRepository();
            var claimList = repository.GetList(c => c.EmployeeID == _employeeNumber);
            if (claimList == null || claimList.Count() == 0) return new List<IClaimTableItem>();
            List<IClaimTableItem> claimItems = new List<IClaimTableItem>();
            foreach (var claim in claimList)
            {
                claimItems.Add(
                    new ClaimsTableItem
                    {
                        ClaimAmount = claim.ClaimAmount,
                        ReceiptAmount = claim.ReceiptAmount,
                        ReceiptDate = claim.ReceiptDate,
                        ReceiptNumber = claim.ReceiptNumber
                    }
                    );
            }
            return claimItems;
        }


        public IClaimResult Update(IClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();

            var dbClaim = repository.Get(c => c.EmployeeID == _employeeNumber && c.ReceiptNumber == claim.ReceiptNumber && c.ReceiptDate == claim.ReceiptDate);
            if (dbClaim == null) return new ClaimResult { IsSuccess = false, Message = ObjectStatus.ObjectNotFound };
            repository.Update(dbClaim, c => c.ReferenceNumber == c.ReferenceNumber);
            return new ClaimResult { IsSuccess = true };
        }


    }
}
