using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Orders;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Data;

/// <summary>
/// Only used for migrations and seeding.
/// </summary>
public class SeedPlusPlusTestContext : SeedPlusPlusContext
{
    public SeedPlusPlusTestContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categories.json");

        var categories = ReadCategoriesFromFile(filePath);

        // var categories = new List<ProductCategory>
        // {
        //     new() { Id = 1, Name = "Root", ParentId = null},
        //     new() { Id = 2, Name = "Electronics", ParentId = 1},
        //     new() { Id = 3, Name = "Computers", ParentId = 2 },
        //     new() { Id = 4, Name = "Apple Computers", ParentId = 3 },
        //     new() { Id = 5, Name = "PCs", ParentId = 3 },
        //     new() { Id = 6, Name = "Smartphones", ParentId = 2 },
        //     new() { Id = 7, Name = "Books", ParentId = 1}
        // };

        var hierarchy = ComputeChildren(categories);
        BuildNestedSet(hierarchy);

        modelBuilder.Entity<ProductCategory>().HasData(categories);
    }

    private static List<ProductCategory> ReadCategoriesFromFile(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        
        var categories = JsonSerializer.Deserialize<List<ProductCategory>>(jsonString);
        
        return categories ?? new List<ProductCategory>();
    }

    private static Dictionary<ProductCategory,HashSet<ProductCategory>> ComputeChildren(List<ProductCategory> categories)
    {
        var hierarchy = categories.ToDictionary(x => x, x => new HashSet<ProductCategory>());
        
        foreach (var category in categories)
        {
            if (category.ParentId is null)
                continue;

            var key = hierarchy.Keys.First(x => x.Id == category.ParentId);
            hierarchy[key].Add(category);
        }

        return hierarchy;
    }

    private static void BuildNestedSet(Dictionary<ProductCategory,HashSet<ProductCategory>> hierarchy)
    {
        var root = hierarchy.Keys.First(c => c.ParentId is null);
        var currentIdx = 1;
        BuildNestedSet(root, ref currentIdx, hierarchy);
    }
    
    private static void BuildNestedSet(ProductCategory current, ref int idx, IReadOnlyDictionary<ProductCategory, HashSet<ProductCategory>> hierarchy)
    {
        current.Left = idx++;
        
        foreach (var child in hierarchy[current])
            BuildNestedSet(child, ref idx, hierarchy);
        
        current.Right = idx++;
    }
}