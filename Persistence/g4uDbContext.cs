using g4u.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace g4u.Persistence
{
    public class g4uDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<UserMessages> UserMessages { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public g4uDbContext(DbContextOptions<g4uDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
        }
    }
}