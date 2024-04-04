using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Entities;
using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Api.Products;

public static class ProductsEndpoints
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/products", GetByCategoryAsync)
            .WithName("GetProducts");
        
        routeBuilder
            .MapGet("/products/{id:int}", GetByIdAsync)
            .WithName("GetProduct");
        
        routeBuilder
            .MapPost("/products", CreateAsync)
            .WithName("CreateProduct");
        
        routeBuilder
            .MapPut("/products/{id:int}", UpdateAsync)
            .WithName("UpdateProduct");
        
        routeBuilder
            .MapDelete("/products/{id:int}", DeleteAsync)
            .WithName("DeleteProduct");

        routeBuilder
            .MapPut("/products/{id:int}:add-image", AddImageAsync)
            .WithName("AddImage");
        
        // TODO
        routeBuilder
            .MapPost("/products/{id:int}/tags", context => throw new NotImplementedException())
            .WithName("AddTag");
        
        return routeBuilder;
    }

    /// <summary>
    /// Gets all the products in the specified category, or all products if category is not specified.
    /// </summary>
    /// <returns>the collection of products</returns>
    private static async Task<Results<Ok<IEnumerable<ProductsResponse>>, NotFound>> GetByCategoryAsync(
        IUseCase<GetProductsInput, Result<IEnumerable<GetProductsOutput>>> handler,
        [FromQuery] int? categoryId)
    {
        return await handler
            .Handle(new GetProductsInput(categoryId))
            .MatchAsync<
                IEnumerable<GetProductsOutput>,
                Results<Ok<IEnumerable<ProductsResponse>>, NotFound>
            >(
                o => TypedResults.Ok(o
                    .Select(p => p.ToProductsResponse())),
                e => TypedResults.NotFound()
                );
    }

    private static async Task<Results<Ok<ProductResponse>, NotFound>> GetByIdAsync(
        int id,
        [FromQuery] bool includeAll,
        IUseCase<GetProductByIdInput, Result<GetProductOutput>> handler
        )
    {
        return (await handler.Handle(new GetProductByIdInput(id, includeAll)))
            .Match<Results<Ok<ProductResponse>, NotFound>>(
                o => TypedResults.Ok(o.ToProductResponse()),
                e => TypedResults.NotFound());
    }

    private static Task<Results<CreatedAtRoute<ProductResponse>, BadRequest>> CreateAsync(
        CreateProductRequest request,
        IUseCase<CreateProductInput, Result<CreateProductOutput>> handler
        )
    {
        return request.ToCreateProductInput()
            .MapAsync(handler.Handle)
            .MapAsync(o => o.ToProductResponse())
            .MatchAsync<ProductResponse, Results<CreatedAtRoute<ProductResponse>, BadRequest>>(
                pr => TypedResults.CreatedAtRoute(pr, "GetProduct", new { pr.Id }),
                e => TypedResults.BadRequest()
            );
    }
    
    private static Task<Results<Ok<ProductResponse>, BadRequest, NotFound>> UpdateAsync(
        int id,
        [FromBody] UpdateProductRequest request,
        IUseCase<UpdateProductInput, Result<UpdateProductOutput>> handler)
    {
        return handler
            .Handle(request.ToUpdateProductInput(id))
            .MapAsync(o => o.ToProductResponse())
            .MatchAsync<ProductResponse, Results<Ok<ProductResponse>, BadRequest, NotFound>>(
            p => TypedResults.Ok(p),
            e => e switch
            {
                NotFoundException<Product> => TypedResults.NotFound(),
                _ => TypedResults.BadRequest()
            }
        );
    }

    // Could also return Conflict if for example rules forbid deletion
    private static Task<Results<NoContent, NotFound>> DeleteAsync(
        int id,
        IUseCase<DeleteProductInput, Result<bool>> handler
        )
    {
        return handler.Handle(new DeleteProductInput(id))
            .MatchAsync<bool, Results<NoContent, NotFound>>(
                r => TypedResults.NoContent(),
                e => TypedResults.NotFound()
            );
    }

    private static Task<Results<NoContent, BadRequest>> AddImageAsync(
        AddImageRequest request,
        IUseCase<AddImageInput, Result<bool>> handler)
    {
        throw new NotImplementedException();
        
        return request.ToAddImageInput()
            .MapAsync(handler.Handle)
            .MatchAsync<bool, Results<NoContent, BadRequest>>(
                o => TypedResults.NoContent(),
                e => TypedResults.BadRequest());
    }
}

public record CreateProductRequest(string Name, decimal Price, int TypeId, int CategoryId,
    ProductImageRequest[] Images, ProductTagRequest[] Tags);
public record ProductImageRequest(int ImageId);
public record ProductTagRequest(int TagId, string TagType, string Value);
public record ProductResponse(
    int Id, string Name, decimal Price, int TypeId, int CategoryId, int NumberInStock,
    ProductImageResponse[] Images, ProductTagResponse[] Tags);
public record ProductsResponse(
    int Id, string Name, decimal Price, int TypeId, int CategoryId, int NumberInStock);
public record UpdateProductRequest(string Name, decimal Price, int TypeId, int CategoryId);
public record AddImageRequest(string ImageType, string Content, string AltText);
public record ProductImageResponse(int ImageId, string AltText, int Index, string ImageType);
public record ProductTagResponse(int TagId, string TagName, string TagType, object Value);
