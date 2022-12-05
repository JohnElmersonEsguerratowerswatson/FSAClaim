using FSA.API.Business.Interfaces;

namespace FSA.API.Business.Model
{
    public class EmployeeModel: IEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
