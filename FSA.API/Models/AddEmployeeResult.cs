using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class AddEmployeeResult : IAddEmployeeResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
