using CastagramV1.Models;
using Microsoft.EntityFrameworkCore;

namespace CastagramV1.Repositories
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
          : base(options) { }

        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
