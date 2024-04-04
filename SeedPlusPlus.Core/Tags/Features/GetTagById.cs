namespace SeedPlusPlus.Core.Tags.Features;

public class GetTagById : IUseCase<GetTagInput, Result<GetTagOutput>>
{
    private readonly ITagRepository _repository;

    public GetTagById(ITagRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetTagOutput>> Handle(GetTagInput input)
    {
        return await _repository.FindById(input.Id)
            .MapAsync(t => new GetTagOutput(
                    Id: t.Id,
                    Name: t.Name,
                    Type: t.Type.ToString().ToLower()
                )
            );
    }
}

public record GetTagInput(int Id);
public record GetTagOutput(int Id, string Name, string Type);
