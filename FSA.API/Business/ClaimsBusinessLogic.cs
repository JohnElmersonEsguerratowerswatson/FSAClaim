using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using FSA.Common.Enums;
using FSA.Common;
using System.Collections.Generic;

namespace FSA.API.Business
{
    public class ClaimsBusinessLogic : IFSAClaimBusiness
    {

        private int _employeeNumber;

        public ClaimsBusinessLogic(int employeeNumber)
        {
            _employeeNumber = employeeNumber;
        }

        private Employee GetEmployee()
        {
            TRepository<Employee> tdb = new TRepository<Employee>();
            return tdb.Get(e => e.ID == _employeeNumber);
        }

        private IEnumerable<FSAClaim> GetApprovedFSAClaimsByEmployee(IEnumerable<FSAClaim> fSAClaims)
        {

            return fSAClaims.Where(ec => ec.EmployeeID == _employeeNumber && (ec.Status == "Approved")).ToList();
        }

        private IEnumerable<FSAClaim> GetPendingFSAClaimsByEmployee(IEnumerable<FSAClaim> fSAClaims)
        {

            return fSAClaims.Where(ec => ec.EmployeeID == _employeeNumber && (ec.Status == "Pending")).ToList();
        }

        private IEnumerable<FSAClaim> GetFSAClaimsByEmployee()
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            return repository.GetList(c => c.EmployeeID == _employeeNumber && c.DateSubmitted.Year == DateTime.Now.Year).ToList();
        }

        private Decimal ComputeApprovedClaims(IEnumerable<FSAClaim> claims)
        {
            return claims.Where(ec => ec.Status == "Approved").Sum(ec => ec.ClaimAmount);
        }

        private Decimal ComputePendingClaims(IEnumerable<FSAClaim> claims)
        {
            return claims.Where(ec => ec.Status == "Pending").Sum(ec => ec.ClaimAmount);
        }

        private FSARule GetFSARule()
        {
            EmployeeFSARepository eFSARepository = new EmployeeFSARepository();

            var employeeFSARule = eFSARepository.Get(e => e.ID == _employeeNumber);

            return employeeFSARule;
        }

        private Decimal ComputeTotalClaims(Decimal approvedClaims, Decimal pendingClaims)
        {

            return approvedClaims + pendingClaims; ;
        }

        private decimal ComputeRemainingFSA(Decimal approvedClaims, Decimal pendingClaims)
        {
            //get Pending and Approved FSAClaims of employee for the (year vs the date submitted)
            var employeeClaims = GetFSAClaimsByEmployee(); //repository.GetList(ec => ec.EmployeeID == _employeeNumber && (ec.Status == "Approved" || ec.Status == "Pending") && ec.DateSubmitted.Year == DateTime.Now.Year);
            //Compute Total Approved Claims for the year
            decimal totalClaims = ComputeTotalClaims(approvedClaims, pendingClaims);

            //employee fsa rule to compare limit to existing

            var employeeFSARule = GetFSARule();
            if (employeeFSARule == null) throw new KeyNotFoundException();

            //Compute Remaining FSA
            decimal remainingFSA = employeeFSARule.FSALimit - totalClaims;

            return remainingFSA;
        }

        public IClaimResult AddClaim(ITransactClaim claim)
        {
            ClaimApprovals approval = ClaimApprovals.Pending;

            var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);

            //Check if Receipt Date is valid
            if (claimReceiptDate.Year != DateTime.UtcNow.Year) approval = ClaimApprovals.Denied; //return new ClaimResult { IsSuccess = false, Message = "BadRequest" };

            //Compose Reference Number
            string refNo = claimReceiptDate.ToString("yyy") + claimReceiptDate.Month.ToString("MM") + claimReceiptDate.Day.ToString("d") + claim.ReceiptNumber;

            FSAClaimRepository repository = new FSAClaimRepository();

            //Compute Remaining FSA
            decimal remainingFSA = 0;
            var fsaClaims = GetFSAClaimsByEmployee();

            decimal approvedClaims = ComputeApprovedClaims(fsaClaims);
            decimal pendingClaims = ComputePendingClaims(fsaClaims);
            try { remainingFSA = ComputeRemainingFSA(approvedClaims, pendingClaims); }
            catch { return new ClaimResult { IsSuccess = false, Message = ObjectStatus.ObjectNotFound }; }

            //Check if FSA amount is greater than Remaining FSA
            if (remainingFSA < claim.ClaimAmount) return new ClaimResult { IsSuccess = false, Message = "You only have " + remainingFSA + "." };

            try
            {
                var claimAdd = CreateClaim(claim.ClaimAmount, claim.ReceiptAmount, claimReceiptDate, claim.ReceiptNumber,approval, refNo, _employeeNumber);

                var result = repository.Add(claimAdd);

                return new ClaimResult { IsSuccess = true, Message = "Submitted for approval." };
            }
            catch { return new ClaimResult { IsSuccess = false, Message = "Server Error: Please file a ticket" }; }

        }

        private FSAClaim CreateClaim(decimal claimAmount, decimal receiptAmount, DateTime receiptDate, string receiptNumber, ClaimApprovals claimApproval, string refNo, int employeeId)
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
            var repositoryResult = repository.Delete(c => c.EmployeeID == _employeeNumber && c.ReferenceNumber == claim.ReferenceNumber);//ew FSAClaim { ReceiptDate = claim.ReceiptDate, ReceiptAmount = claim.ReceiptAmount}
            if (!repositoryResult.IsSuccess) return new ClaimResult { IsSuccess = false, Message = repositoryResult.Error.Message };
            return new ClaimResult { IsSuccess = true };
        }

        public IViewClaim GetClaim(string referenceNumber)
        {
            FSAClaimRepository repository = new FSAClaimRepository();
            var claim = repository.Get(c => c.ReferenceNumber == referenceNumber);
            //var claimReceiptDate = DateTime.Parse(claim.ReceiptDate);
            if (claim == null) return null;
            return new EmployeeClaim { ClaimAmount = claim.ClaimAmount, ReceiptAmount = claim.ReceiptAmount, ReceiptDate = claim.ReceiptDate.ToString("MM/dd/yyyy"), ReceiptNumber = claim.ReceiptNumber, DateSubmitted = claim.DateSubmitted.ToString("MM/dd/yyyy"), ReferenceNumber = claim.ReferenceNumber.ToString(), Status = claim.Status, TotalClaimAmount = claim.ClaimAmount };
        }
        /// <summary>
        /// GET LIST VIEW LOGIC
        /// </summary>
        /// <returns></returns>
        public IGetClaimsResult GetClaimsResult()
        {
            try
            {
                GetClaimsResult result = new GetClaimsResult();
                var claims = GetFSAClaimsByEmployee();
                result.Claims = GetClaimList();
                result.EmployeeID = _employeeNumber;
                var fsaRule = GetFSARule();
                if (fsaRule == null) fsaRule = new FSARule();
                result.FSAAmount = fsaRule.FSALimit;
                result.YearCoverage = fsaRule.YearCoverage;
                var employee = GetEmployee();

                result.EmployeeName = employee.FirstName + " " + employee.LastName;
                result.ApprovedClaims = ComputeApprovedClaims(claims);
                result.PendingClaims = ComputePendingClaims(claims);


                result.AvailableFSA = ComputeRemainingFSA(result.ApprovedClaims, result.PendingClaims);

                return result;
            }
            catch
            {
                return new GetClaimsResult();
            }
        }


        public List<ClaimsTableItem> GetClaimList()
        {
            //FSAClaimRepository repository = new FSAClaimRepository();
            var claimList = GetFSAClaimsByEmployee();// repository.GetList(c => c.EmployeeID == _employeeNumber);

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
