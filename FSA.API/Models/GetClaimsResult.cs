using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class GetClaimsResult : IGetClaimsResult
    {
        public IEnumerable<IViewClaim> Claims { get; set; }
        public decimal FSAAmount { get; set; }
        public int YearCoverage { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal AvailableFSA {get; set; }
        public decimal PendingClaims { get; set; }
        public decimal ApprovedClaims { get; set; }
    }
}
