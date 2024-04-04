namespace SeedPlusPlus.Core.Products.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
    public int TypeId { get; set; }
    public ProductCategory Category { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<ProductImage> ProductImages { get; set; } = Enumerable.Empty<ProductImage>();
    public IEnumerable<ProductTag> ProductTags { get; set; } = Enumerable.Empty<ProductTag>();
    // TODO: Keep or remove navigation?
    public IEnumerable<StockKeepingUnit> Skus { get; set; } = Enumerable.Empty<StockKeepingUnit>();
    public int NumberInStock { get; set; }
    public bool IsInStock => NumberInStock > 0;

    // TODO: Set IsInStock for every update to Skus
}
