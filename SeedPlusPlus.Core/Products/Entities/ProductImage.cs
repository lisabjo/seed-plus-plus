namespace SeedPlusPlus.Core.Products.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Image Image { get; set; }
    public int ImageId { get; set; }
    /// <summary>
    /// For ordering the ProductImages associates with an Image
    /// </summary>
    public int Index { get; set; }
}
