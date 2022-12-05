namespace FSA.API.Models.Interface
{
    public interface IAddFSARuleResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
