using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.DBContext
{
    internal class FSAClaimContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasMany<FSAClaim>(e => e.FSAClaims).WithOne();
            modelBuilder.Entity<EmployeeFSA>().HasNoKey();
            modelBuilder.Entity<Login>().HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=PCM-6H43TL3\\SQLEXPRESS; Initial Catalog=FSAClaims; Integrated Security=true; Encrypt=false");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<FSARule> FSARules { get; set; }

        public DbSet<EmployeeFSA> EmployeeFSAs { get; set; }

        public DbSet<FSAClaim> FSAClaims { get; set; }

        public DbSet<Login> Logins { get; set; }

    }
}
