using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Models;
using FSA.Data.Repository.LoginRepository;

namespace FSA.API.Business
{
    public class LoginLogic
    {
        public IValidatedLogin ValidateLogin(LoginModel loginModel)
        {
            LoginRepository loginRepository = new LoginRepository();
            var login = loginRepository.Get(l => l.Username == loginModel.Username && l.Password == loginModel.Password);
            if(login == null) return null;
            return new ValidatedLogin(login.EmployeeID, login.Role);
        }
    }
}
