using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace Global_Intern.Models
{
    public class GlobalDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder
                .UseSqlServer(@"Data Source = (localdb)\ProjectsV13; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<AppliedInternship> AppliedInternships { get; set; }
        public DbSet<InternStudent> InternStudents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<VisaStatus> VisaStatuses { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
    }
}
