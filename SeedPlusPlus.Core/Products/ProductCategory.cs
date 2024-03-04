namespace SeedPlusPlus.Core.Products;

public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    public int? ParentId { get; set; }
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory>? Children { get; set; }
}