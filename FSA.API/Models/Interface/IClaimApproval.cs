namespace FSA.API.Models.Interface
{
    public interface IClaimApproval
    {
        public bool Approve { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
