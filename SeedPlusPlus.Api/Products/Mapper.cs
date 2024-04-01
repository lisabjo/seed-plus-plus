using SeedPlusPlus.Core.Products;
using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Api.Products;

public static class Mapper
{
    public static AddImageInput ToAddImageInput(this AddImageRequest request)
    {
        return new AddImageInput
        (
            Type: Enum.Parse<ImageType>(request.ImageType),
            Content: Convert.FromBase64String(request.Content),
            AltText: request.AltText
        );
    }
}