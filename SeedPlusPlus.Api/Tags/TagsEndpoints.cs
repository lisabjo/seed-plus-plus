using Microsoft.AspNetCore.Http.HttpResults;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Tags.Features;

namespace SeedPlusPlus.Api.Tags;

public static class TagsEndpoints
{
    public static IEndpointRouteBuilder MapTagsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/tags", GetAll)
            .WithName("GetAllTags");
        
        routeBuilder
            .MapGet("/tags/{id:int}", Get)
            .WithName("GetTag");
        
        routeBuilder
            .MapPost("/tags", CreateTag)
            .WithName("CreateTag");
        
        routeBuilder
            .MapDelete("/tags/{id:int}", DeleteTag)
            .WithName("DeleteTag");
        
        return routeBuilder;
    }

    private static Task<Results<Ok<IEnumerable<TagResponse>>, NotFound>> GetAll(
        IUseCase<GetAllTagsInput, Result<GetAllTagsOutput>> handler
        )
    {
        return handler.Handle(new GetAllTagsInput())
            .MatchAsync<GetAllTagsOutput, Results<Ok<IEnumerable<TagResponse>>, NotFound>>(
                x => TypedResults.Ok(x.Tags.Select(t => t.ToTagResponse())),
                e => TypedResults.NotFound()
            );
    }

    private static async Task<Results<Ok<TagResponse>, NotFound>> Get(
        int id,
        IUseCase<GetTagInput, Result<GetTagOutput>> handler)
    {
        return (await handler.Handle(new GetTagInput(id)))
            .Match<Results<Ok<TagResponse>, NotFound>>(
                t => TypedResults.Ok(t.ToTagResponse()),
                (e) => TypedResults.NotFound()
            );
    }

    private static async Task<Results<CreatedAtRoute<TagResponse>, BadRequest>> CreateTag(
        CreateTagRequest request,
        IUseCase<CreateTagInput, Result<CreateTagOutput>> handler)
    {
        return await request
            .ToCreateTagInput()
            .MapAsync(handler.Handle)
            .MapAsync(tag => tag.ToTagResponse())
            .MatchAsync<TagResponse, Results<CreatedAtRoute<TagResponse>, BadRequest>>(
                tr => TypedResults.CreatedAtRoute(tr, "GetTag", new { tr.Id }),
                e => TypedResults.BadRequest()
            );
    }

    private static Task<Results<NoContent, NotFound>> DeleteTag(
        int id,
        IUseCase<DeleteTagInput, Result<bool>> handler)
    {
        return handler.Handle(new DeleteTagInput(id))
            .MatchAsync<bool, Results<NoContent, NotFound>>(
                b => TypedResults.NoContent(),
                e => TypedResults.NotFound()
            );
    }
}

public record TagResponse(int Id, string Name, string Type);
public record CreateTagRequest(string Name, string Type);