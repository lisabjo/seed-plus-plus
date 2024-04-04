using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeedPlusPlus.Api.Products;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Products.Features;

namespace SeedPlusPlus.Api.Categories;

public static class CategoriesEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/categories", GetAllCategoriesAsync)
            .WithName("GetCategories");

        routeBuilder
            .MapPost("/categories", CreateCategoryAsync)
            .WithName("CreateCategory");

        return routeBuilder;
    }

    private static async Task<Results<Ok<IEnumerable<CategoryResponse>>, NotFound>> GetAllCategoriesAsync(
        [FromQuery] int? parentId,
        IUseCase<GetCategoriesInput, Result<IEnumerable<CategoryOutput>>> handler
        )
    {
        return await handler.Handle(new GetCategoriesInput(parentId))
            .MatchAsync<IEnumerable<CategoryOutput>, Results<Ok<IEnumerable<CategoryResponse>>, NotFound>>(
                o => TypedResults.Ok(o
                    .Select(p => p.ToCategoryResponse())),
                e => TypedResults.NotFound()
            );
    }
    
    private static Task<Results<CreatedAtRoute<CategoryResponse>, BadRequest>> CreateCategoryAsync(
        CreateCategoryRequest request,
        IUseCase<CreateCategoryInput, Result<CategoryOutput>> handler
        )
    {
        return request.ToCreateCategoryInput()
            .MapAsync(handler.Handle)
            .MapAsync(o => o.ToCategoryResponse())
            .MatchAsync<CategoryResponse, Results<CreatedAtRoute<CategoryResponse>, BadRequest>>(
                cr => TypedResults.CreatedAtRoute(cr, "GetProduct", new { cr.Id }),
                e => TypedResults.BadRequest()
            );
    }
}

public record CategoryResponse(int Id, string Name, int Left, int Right, int? ParentId);
public record CreateCategoryRequest(string Name, int ParentId);
