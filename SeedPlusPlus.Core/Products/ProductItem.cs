using SeedPlusPlus.Core.Products.Seeds;

namespace SeedPlusPlus.Core.Products;

public class ProductItem
{
    public int Id { get; set; }
    public Variety Variety { get; set; }
    public Batch Batch { get; set; }
}