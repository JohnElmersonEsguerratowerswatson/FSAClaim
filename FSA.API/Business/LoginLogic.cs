using FSA.Data.Repository.LoginRepository;

namespace FSA.API.Business
{
    public class LoginLogic
    {
        public LoginLogic()
        {
            LoginRepository loginRepository = new LoginRepository();
            loginRepository.Get(l => l.Username == "" && l.Password == "");
        }
    }
}
