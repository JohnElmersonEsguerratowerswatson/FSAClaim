using FSA.API.Business.Interfaces;

namespace FSA.API.Business.Model
{
    public class ValidatedLogin : IValidatedLogin
    {
        private readonly int _id;
        public int ID { get { return _id; } }

        private readonly string _role;
        public string Role { get { return _role; } }


        public ValidatedLogin(int id, string name)
        {
            _id = id;
            _role = name;
        }
    }
}
