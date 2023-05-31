
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CrmAssistant.Models
{
    public class PubContext : DbContext
    {
        public PubContext(DbContextOptions<PubContext> options) : base(options)
        {

        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Student & StudentAddress entity
            modelBuilder.Entity<User>()
                        .HasOne(u => u.Address) 
                        .WithOne(ad => ad.User)
                        .HasForeignKey<Address>(); 
        }
        */
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
    }
}
