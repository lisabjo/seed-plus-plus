using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Products.Entities;
using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Data;

public class SeedPlusPlusContext : DbContext
{
    public SeedPlusPlusContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Product> Products { get; init; }
    public DbSet<ProductCategory> ProductCategories { get; init; }
    public DbSet<StockKeepingUnit> Skus { get; init; }
    public DbSet<Tag> Tags { get; init; }
    public DbSet<Image> Images { get; init; }

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

        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductTags)
            .WithOne(t => t.Product);
        
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)
            .WithOne(t => t.Product);
    }
}