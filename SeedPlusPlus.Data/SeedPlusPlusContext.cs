using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Data;

public class SeedPlusPlusContext : DbContext
{
    public SeedPlusPlusContext(DbContextOptions options) : base(options) { }
    
    // public DbSet<Order> Orders { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            // .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
            ;
    }
}