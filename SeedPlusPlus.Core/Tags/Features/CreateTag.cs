namespace SeedPlusPlus.Core.Tags.Features;

public class CreateTag : IUseCase<CreateTagInput, Result<CreateTagOutput>>
{
    private readonly ITagRepository _repository;

    public CreateTag(ITagRepository repository) => _repository = repository;

    public Task<Result<CreateTagOutput>> Handle(CreateTagInput input)
    {
        return _repository
            .AddTag(new Tag { Name = input.Name, Type = input.Type })
            .MapAsync(t => new CreateTagOutput(t)
            );
    }
}

public record CreateTagInput(string Name, TagType Type);
public record CreateTagOutput(Tag Tag);
