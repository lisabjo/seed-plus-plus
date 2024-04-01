namespace SeedPlusPlus.Core.Products;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public HashSet<Tag> Tags { get; set; } = new();
}