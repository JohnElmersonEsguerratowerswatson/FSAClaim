using AutoFixture;
using FSA.Data.DBContext;
using FSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Test.DataTest
{
    internal class DataTestHelper
    {

        public static void SeedEmployees(FSAClaimContext dbContext)
        {
            var fixture = new Fixture();
            List<Employee> employees;
            employees = fixture.Create<List<Employee>>();
            employees.ForEach(e => e.ID = 0);

            dbContext.Employees.AddRange(employees);
            dbContext.SaveChanges();
        }

        public static void SeedLogin(FSAClaimContext dbContext)
        {
            List<Employee> employees = dbContext.Employees.ToList();
            List<Login> logins = new List<Login>();
            employees.ForEach(e =>
                logins.Add(
                    new Login
                    {
                        EmployeeID = e.ID,
                        Username = Guid.NewGuid().ToString(),
                        Password = "Password123",
                        Role = "User"
                    }
                    )
            );
            dbContext.Logins.AddRange(logins);
            dbContext.SaveChanges();
        }


        public static void SeedClaims(FSAClaimContext dbContext)
        {
            List<Employee> employees = dbContext.Employees.ToList();
            var fixture = new Fixture();
            List<FSAClaim> fSAClaims = new List<FSAClaim>();
            employees.ForEach(e =>
               fSAClaims.Add(
                  fixture.Build<FSAClaim>().Without(c=>c.ID).Do(c => { c.ID = 0; c.EmployeeID = e.ID; c.DateSubmitted = DateTime.UtcNow; c.ReferenceNumber = Guid.NewGuid().ToString(); c.Status = "Pending"; }).Create()
                   )
           );
            dbContext.FSAClaims.AddRange(fSAClaims);
            dbContext.SaveChanges();
        }


        public static FSAClaim GenerateClaim()
        {
            var fixture = new Fixture();
            var claim = fixture.Create<FSAClaim>();
            claim.ReferenceNumber = Guid.NewGuid().ToString();
            claim.ID = 0;
            return claim;
        }


        public static FSARule GenerateFSARule()
        {
            var fixture = new Fixture();
            var rule = fixture.Create<FSARule>();
            return rule;
        }
    }
}
