using Microsoft.AspNetCore.Mvc;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Api.Endpoints;

public static class Products
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/products/{id}", GetProductByIdAsync)
            .WithName("GetProduct");
        
        routeBuilder
            .MapPost("/products", CreateProductAsync)
            .WithName("CreateProduct");
        
        routeBuilder
            .MapPut("/products/{id}", UpdateProductAsync)
            .WithName("UpdateProduct");
        
        routeBuilder
            .MapDelete("/products/{id}", RemoveProductByIdAsync)
            .WithName("DeleteProduct");
        
        return routeBuilder;
    }

    public static async Task<ActionResult<Product>> GetProductByIdAsync(int id, IProductCategoryRepository repo)
    {
        return new Product();
    }

    public static async Task<ActionResult<string>> UpdateProductAsync(int id)
    {
        return $"Updating product with ID: {id}";
    }
    
    public static async Task RemoveProductByIdAsync(int id, IProductCategoryRepository repo)
    {
        
    }
    
    public static async Task<ActionResult<Product>> CreateProductAsync(IProductCategoryRepository repo)
    {
        return new Product();
    }
}