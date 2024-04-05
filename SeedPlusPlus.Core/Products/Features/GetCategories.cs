using SeedPlusPlus.Core.Products.Contracts;

namespace SeedPlusPlus.Core.Products.Features;

public class GetCategories : IUseCase<GetCategoriesInput, Result<IEnumerable<CategoryOutput>>>
{
    private readonly IProductRepository _repository;

    public GetCategories(IProductRepository repository) => _repository = repository;

    public async Task<Result<IEnumerable<CategoryOutput>>> Handle(GetCategoriesInput input)
    {
        var categories = await _repository
            .FindCategoryById(input.ParentId ?? 0)
            .MatchAsync(
                c => _repository.GetAllCategoriesAsync(c), 
                e => _repository.GetAllCategoriesAsync());

        return (await categories)
            .Map(c => c
            .Select(pc => new CategoryOutput(
                Id: pc.Id,
                Name: pc.Name,
                Left: pc.Left,
                Right: pc.Right,
                ParentId: pc.ParentId
            )));
    }
}

public record GetCategoriesInput(int? ParentId);
public record CategoryOutput(int Id, string Name, int Left, int Right, int? ParentId);
