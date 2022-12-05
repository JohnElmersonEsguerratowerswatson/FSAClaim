using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Models;
using FSA.Data.Repository;
using FSA.Data.Repository.LoginRepository;
using FSA.Domain.Entities;

namespace FSA.API.Business
{
    public class LoginLogic : ILoginService
    {
        private IViewRepository<Login> _loginRepository;

        public LoginLogic(IViewRepository<Login> loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public IValidatedLogin ValidateLogin(LoginModel loginModel)
        {
           //LoginRepository loginRepository = new LoginRepository();
            var login = _loginRepository.Get(l => l.Username == loginModel.Username && l.Password == loginModel.Password);
            if (login == null) return null;
            return new ValidatedLogin(login.EmployeeID, login.Role);
        }
    }
}
