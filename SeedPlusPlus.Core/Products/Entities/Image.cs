namespace SeedPlusPlus.Core.Products.Entities;

public class Image
{
    public int Id { get; set; }
    public ImageType Type { get; set; }
    public byte[] Content { get; set; }
    public string Alt { get; set; }
}

public enum ImageType
{
    Png,
    Jpeg
}
