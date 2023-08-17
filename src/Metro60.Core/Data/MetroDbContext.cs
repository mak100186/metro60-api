using kDg.FileBaseContext.Extensions;

using Metro60.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Metro60.Core.Data;

public class MetroDbContext : DbContext
{
    public MetroDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    
    //notes for reviewer: Since there is no db to run the constraints, this wont work for file based databases. Need to manually ensure constraint.
    //only added these to demonstrate how they would have worked in a real-world situation. 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.Title, p.Brand }).IsUnique();

        modelBuilder.Entity<Product>()
            .ToTable(p => p.HasCheckConstraint("CK_Prices", "[Price] > 0"));
    }
}
