using GetPhone.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GetPhone.Database;

public class ApplicationContext : DbContext {
    public DbSet<Phone> phones {get; set;} = null!;
    public DbSet<Review> reviews {get; set;} = null!;
    public DbSet<Admin> admins {get; set;} = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(
            new Admin {Id = 1, Username = "admin", Email = "admin@example.com", PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8" }
        );
    }
}

