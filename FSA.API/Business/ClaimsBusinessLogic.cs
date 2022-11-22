using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using System.Security.Claims;

namespace FSA.API.Business
{
    public class ClaimsBusinessLogic : IFSAClaimBusiness
    {
        public IClaimResult AddClaim(IClaim claim, int id)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            TRepository<FSARule> tRepo = new TRepository<FSARule>();
            //var employee

            if (claim == null) return new ClaimResult { IsSuccess = false, Message = "BadRequest" };
            if (claim.ReceiptDate.Year != DateTime.UtcNow.Year) return new ClaimResult { IsSuccess = false, Message = "BadRequest" };

            var employeeClaims = repository.GetList(ec => ec.EmployeeID == id && ec.Status == "Approved" && ec.ApprovalDate.Year == DateTime.Now.Year);
            decimal totalClaims = employeeClaims.Sum(ec => ec.ClaimAmount);
            string refNo = claim.ReceiptDate.Year.ToString("yyy") + claim.ReceiptDate.Month.ToString("MM") + claim.ReceiptNumber.ToString();
            //employee fsa rule to compare limit to existing
            EmployeeFSARepository eFSARepository = new EmployeeFSARepository();
            var employeeFSARule = eFSARepository.Get(e => e.ID == id);
            if (employeeFSARule == null) return new ClaimResult { IsSuccess = false, Message = "FSA NotFound" };
            decimal remainingFSA = employeeFSARule.FSALimit - totalClaims;

            if (remainingFSA < claim.ClaimAmount) return new ClaimResult { IsSuccess = false, Message = "You only have " + remainingFSA + "." };


            try
            {
                var result = repository.Add(new FSAClaim
                {
                    ApprovalDate = DateTime.UtcNow,
                    ClaimAmount = claim.ClaimAmount,
                    DateSubmitted = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                    ReceiptAmount = claim.ReceiptAmount,
                    ReceiptDate = claim.ReceiptDate,
                    ReceiptNumber = claim.ReceiptNumber,
                    Status = "Pending",
                    EmployeeID = id,//get from db or User .Net variable
                    ReferenceNumber = int.Parse(refNo)// generate a unique number

                });
                return new ClaimResult { IsSuccess = true, Message = "" };
            }
            catch { return new ClaimResult { IsSuccess = false, Message = "Server Error: Please file a ticket" }; }

        }

        public IClaimResult Delete(IClaim claim)
        {
            throw new NotImplementedException();
        }

        public IClaim GetClaim(int referenceNumber)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claim = repository.Get(c => c.ReferenceNumber == referenceNumber);
            if (claim == null) return null;
            return new EmployeeClaim { ClaimAmount = claim.ClaimAmount, ReceiptAmount = claim.ReceiptAmount, ReceiptDate = claim.ReceiptDate, ReceiptNumber = claim.ReceiptNumber };
        }

        public List<IClaimTableItem> GetClaimList(int employeeID)
        {
            throw new NotImplementedException();
        }

        public IClaimResult Update(IClaim claim)
        {
            FSAClaimRepository repository = new FSAClaimRepository();

            throw new NotImplementedException();
        }
    }
}
