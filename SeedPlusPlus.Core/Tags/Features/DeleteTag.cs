namespace SeedPlusPlus.Core.Tags.Features;

public class DeleteTag : IUseCase<DeleteTagInput, Result<bool>>
{
    private readonly ITagRepository _repository;

    public DeleteTag(ITagRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<bool>> Handle(DeleteTagInput input)
    {
        return await _repository.DeleteTagAsync(input.Id);
    }
}

public record DeleteTagInput(int Id);