namespace SeedPlusPlus.Core.Tags.Features;

public class CreateTag : IUseCase<CreateTagInput, Result<CreateTagOutput>>
{
    private readonly ITagRepository _repository;

    public CreateTag(ITagRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Result<CreateTagOutput>> Handle(CreateTagInput input)
    {
        var tag = new Tag
        {
            Name = input.Name,
            Type = input.Type
        };
        return _repository.AddTag(tag)
            .MapAsync(t => new CreateTagOutput(
                    Id: t.Id,
                    Name: t.Name,
                    Type: t.Type.ToString().ToLower()
                )
            );
    }
}

public record CreateTagInput(string Name, TagType Type);
public record CreateTagOutput(int Id, string Name, string Type);
