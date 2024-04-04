using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Core.Products.Entities;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public HashSet<Tag> Tags { get; set; } = new();
}