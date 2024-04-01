namespace SeedPlusPlus.Api.Products;

public record AddImageRequest
{
    public string ImageType { get; set; }
    public string Content { get; set; }
    public string AltText { get; set; }
}