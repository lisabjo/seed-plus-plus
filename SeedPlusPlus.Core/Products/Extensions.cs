using SeedPlusPlus.Core.Products.Entities;
using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Core.Products;

internal static class Helpers
{
    internal static ProductImageOutput ToProductImageOutput(this ProductImage pi)
    {
        return new ProductImageOutput(
            ImageId: pi.ImageId,
            AltText: pi.Image.Alt,
            Index: pi.Index,
            ImageType: pi.Image.Type.ToString().ToLower()
        );
    }
}