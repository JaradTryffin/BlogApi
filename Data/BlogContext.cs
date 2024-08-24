using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data;

public class BlogContext:DbContext
{
     public BlogContext(DbContextOptions<BlogContext> options) : base(options){}
     
     public DbSet<User> Users { get; set; }
     public DbSet<Post> Posts { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.Entity<User>()
               .HasMany(u => u.Posts)
               .WithOne(p => p.User)
          .HasForeignKey(p => p.UserId);
     }
     
}