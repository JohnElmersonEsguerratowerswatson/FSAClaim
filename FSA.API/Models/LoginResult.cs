using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class LoginResult : ILoginResult
    {
        public string Bearer {get;set;}
    }
}
