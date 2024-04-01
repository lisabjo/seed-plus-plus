namespace SeedPlusPlus.Core.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public int CategoryId { get; set; }
    public ProductType Type { get; set; }
    public int TypeId { get; set; }
    public List<StockKeepingUnit> Skus { get; set; } = new();
}