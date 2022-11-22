using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class ClaimResult : IClaimResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
