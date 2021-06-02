using Microsoft.EntityFrameworkCore;

namespace BRD_Sem.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Expired> Expireds { get; set; }
    }
}