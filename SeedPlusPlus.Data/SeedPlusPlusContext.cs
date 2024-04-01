using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Data;

public class SeedPlusPlusContext : DbContext
{
    public SeedPlusPlusContext(DbContextOptions options) : base(options) { }
    
    // public DbSet<Order> Orders { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<ProductCategory> ProductCategories { get; init; }
    public DbSet<StockKeepingUnit> Skus { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            // .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
            ;

        modelBuilder.Entity<Tag>()
            .HasMany<ProductType>()
            .WithMany(pt => pt.Tags);
    }
}