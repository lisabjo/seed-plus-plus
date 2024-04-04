using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Tags;
using SeedPlusPlus.Core.Tags.Features;

namespace SeedPlusPlus.Api.Tags;

public static class Mapper
{
    public static Result<CreateTagInput> ToCreateTagInput(this CreateTagRequest request)
    {
        return Enum.TryParse<TagType>(request.Type, true, out var type)
            ? new CreateTagInput(request.Name, type)
            : new Exception("Invalid TagType"); // TODO: Create an Exception
    }

    public static TagResponse ToTagResponse(this CreateTagOutput output)
    {
        return new TagResponse(
            Id: output.Id,
            Name: output.Name,
            Type: output.Type.ToLower()
        );
    }
    
    public static TagResponse ToTagResponse(this GetTagOutput output)
    {
        return new TagResponse(
            Id: output.Id,
            Name: output.Name,
            Type: output.Type.ToLower()
        );
    }
}