using SeedPlusPlus.Core.Products.Seeds;

namespace SeedPlusPlus.Core.Products;

public class Product
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;  // Seed batch information?
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public int CategoryId { get; set; }
    public Variety Variety { get; set; }
}