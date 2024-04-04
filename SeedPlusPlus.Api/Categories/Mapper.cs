using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Api.Categories;

public static class Mapper
{
    public static CategoryResponse ToCategoryResponse(this CategoryOutput output)
    {
        return new CategoryResponse(
            Id: output.Id,
            Name: output.Name,
            Left: output.Left,
            Right: output.Right,
            ParentId: output.ParentId
        );
    }
}