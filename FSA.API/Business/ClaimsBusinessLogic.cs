using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Domain.Entities;
using System.Security.Claims;

namespace FSA.API.Business
{
    public class ClaimsBusinessLogic : IFSAClaimBusiness
    {
        public bool AddClaim(IClaim claim, int id)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            //var employee
            string refNo = claim.ReceiptDate.Year.ToString("yyy") + claim.ReceiptDate.Month.ToString("MM") + claim.ReceiptNumber.ToString();

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
                return result.IsSuccess;
            }
            catch { return false; }

        }

        public bool Delete(IClaim claim)
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

        public bool Update(IClaim claim)
        {
            throw new NotImplementedException();
        }
    }
}
