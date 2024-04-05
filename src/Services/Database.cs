using GetPhone.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GetPhone.Database;

public class ApplicationContext : DbContext {
    public DbSet<Phone> phones {get; set;} = null!;
    public DbSet<Review> reviews {get; set;} = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(
            new Admin {Id = 1, Username = "admin", Email = "admin@example.com", PasswordHash = "fc8252c8dc55839967c58b9ad755a59b61b67c13227ddae4bd3f78a38bf394f7" }
        );
    }
}

