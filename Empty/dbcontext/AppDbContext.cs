using Empty.models;
using Microsoft.EntityFrameworkCore;
namespace Empty.dbcontext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) { }
        public DbSet<User> Users { get; set; }


    }
}
