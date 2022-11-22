namespace FSA.API.Models.Interface
{
    public interface IClaimResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
