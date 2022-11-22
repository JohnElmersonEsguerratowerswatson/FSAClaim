namespace FSA.API.Models.Interface
{
    public interface IViewClaim
    {
        public string ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal ClaimAmount { get; set; }

        public string ReferenceNumber { get; set; }
        public string DateSubmitted { get; set; }
        public String Status { get; set; }
        public decimal TotalClaimAmount { get; set; }

    }
}
