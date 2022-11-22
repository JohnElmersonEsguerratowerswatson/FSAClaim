namespace FSA.API.Models.Interface
{
    public interface ITransactClaim
    {
        public string ReferenceNumber { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public string ReceiptDate { get; set; }
    }
}