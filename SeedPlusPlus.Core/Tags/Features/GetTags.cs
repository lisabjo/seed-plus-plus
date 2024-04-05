namespace SeedPlusPlus.Core.Tags.Features;

public class GetTags : IUseCase<GetAllTagsInput, Result<GetAllTagsOutput>>
{
    private readonly ITagRepository _repository;

    public GetTags(ITagRepository repository) => _repository = repository;

    public Task<Result<GetAllTagsOutput>> Handle(GetAllTagsInput input)
    {
        return _repository.GetAllAsync()
            .MapAsync(t => new GetAllTagsOutput(t));
    }
}

public record GetAllTagsInput;
public record GetAllTagsOutput(IEnumerable<Tag> Tags);
