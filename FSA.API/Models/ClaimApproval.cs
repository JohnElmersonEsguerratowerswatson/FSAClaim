using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class ClaimApproval : IClaimApproval
    {
        public bool Approve { get; set; }
        public string ReferenceNumber { get; set; }
    }

}
