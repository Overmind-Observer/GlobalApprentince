using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Global_Intern.Models;

namespace Global_Intern.Data
{
    public class Global_InternContext : DbContext
    {
        public Global_InternContext (DbContextOptions<Global_InternContext> options)
            : base(options)
        {
        }

        public DbSet<Global_Intern.Models.User> User { get; set; }
    }
}
