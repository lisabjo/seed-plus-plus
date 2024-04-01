using SeedPlusPlus.Data;

namespace SeedPlusPlus.Api.Categories;

public static class Categories
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/categories", GetAllCategoriesAsync)
            .WithName("GetCategories");

        return routeBuilder;
    }

    private static async Task<List<string>> GetAllCategoriesAsync(SeedPlusPlusContext ctx)
    {
        return ctx.ProductCategories.Select(pc => pc.Name).ToList();
    }
}