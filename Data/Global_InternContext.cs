using Microsoft.EntityFrameworkCore;

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
