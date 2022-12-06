using FSA.API.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace FSA.API.Models
{
    public class TransactClaim : ITransactClaim
    {
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public decimal ClaimAmount { get; set; }
        [StringLength(25)]
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        [StringLength(10)]
        public string ReceiptDate { get; set; }
    }
}