using FSA.API.Models;

namespace FSA.API.Business
{
    public interface IClaimsApprovalService
    {
        public bool ApproveClaim(ClaimApproval claimApproval);
        public List<ClaimsApprovalTableItems> GetTableView();
        
    }
}