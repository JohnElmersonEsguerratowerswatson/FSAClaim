using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class AddFSARuleResult : IAddFSARuleResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}