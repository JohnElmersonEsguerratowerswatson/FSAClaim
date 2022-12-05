using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSA.Data.DBContext
{
    public interface IFSAClaimContext
    {
        DbSet<EmployeeFSA> EmployeeFSAs { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<FSAClaim> FSAClaims { get; set; }
        DbSet<FSARule> FSARules { get; set; }
        DbSet<Login> Logins { get; set; }
    }
}