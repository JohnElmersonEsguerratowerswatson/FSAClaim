using FSA.API.Business.Interfaces;
using FSA.API.Models;

namespace FSA.API.Business
{
    public interface ILoginService
    {
        IValidatedLogin ValidateLogin(LoginModel loginModel);
    }
}