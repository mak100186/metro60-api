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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseFileBaseContextDatabase("DataSource");
        base.OnConfiguring(optionsBuilder);
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();
        
        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.Title, p.Brand }).IsUnique();

        //modelBuilder.Entity<Product>()
        //    .OwnsOne(x => x.Images);

        modelBuilder.Entity<Product>()
            .ToTable(p => p.HasCheckConstraint("CK_Prices", "[Price] > 0"));
    }
}
