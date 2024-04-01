using Microsoft.AspNetCore.Mvc;
using SeedPlusPlus.Core.Products;
using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Api.Products;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/products", GetProductsByCategory)
            .WithName("GetProducts");
        
        routeBuilder
            .MapGet("/products/{id:int}", GetProductByIdAsync)
            .WithName("GetProduct");
        
        routeBuilder
            .MapPost("/products", CreateProductAsync)
            .WithName("CreateProduct");
        
        routeBuilder
            .MapPut("/products/{id:int}", UpdateProductAsync)
            .WithName("UpdateProduct");
        
        routeBuilder
            .MapDelete("/products/{id:int}", RemoveProductByIdAsync)
            .WithName("DeleteProduct");

        routeBuilder
            .MapPost("/products/{id:int}", AddImageToProduct)
            .WithName("AddImage");
        
        return routeBuilder;
    }

    /// <summary>
    /// Gets all the products in the specified category, or all products if category is not specified.
    /// </summary>
    /// <returns>the list of products</returns>
    private static async Task<ActionResult<List<Product>>> GetProductsByCategory(
        IProductRepository repo,
        [FromQuery] int categoryId = 0)
    {
        var category = await repo.FindCategoryById(categoryId);

        return await repo.GetAllFromCategoryAsync(category);
    }

    private static async Task<ActionResult<bool>> GetProductByIdAsync(int id, IProductRepository repo)
    {
        var product = await repo.FindById(id);
        return await repo.IsInStock(product);
    }

    private static async Task<ActionResult<string>> UpdateProductAsync(int id)
    {
        return $"Updating product with ID: {id}";
    }

    private static async Task RemoveProductByIdAsync(int id, IProductRepository repo)
    {
        
    }

    private static async Task<ActionResult<Product>> CreateProductAsync([FromBody] CreateProductRequest request, IProductRepository repo)
    {
        
        
        return new Product();
    }

    private static async Task<IResult> AddImageToProduct([FromBody] AddImageRequest request)
    {
        var handler = new AddImage();
        await handler.Handle(request.ToAddImageInput());
        return Results.NoContent();
    }
}