using Microsoft.EntityFrameworkCore;

namespace CrmAssistant.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public virtual DbSet<User> Users { get; set; } = default!;
        public virtual DbSet<Address> Addresses { get; set; } = default!;
    }
}
