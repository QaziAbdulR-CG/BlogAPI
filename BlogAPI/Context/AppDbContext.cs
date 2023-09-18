using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
