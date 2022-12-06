namespace FSA.API.Models.Interface
{
    public interface IAddEmployeeResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
