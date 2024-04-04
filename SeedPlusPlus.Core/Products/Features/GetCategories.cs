using SeedPlusPlus.Core.Products.Contracts;

namespace SeedPlusPlus.Core.Products.Features;

public class GetCategories : IUseCase<GetCategoriesInput, Result<IEnumerable<CategoryOutput>>>
{
    private readonly IProductRepository _repository;

    public GetCategories(IProductRepository repository) => _repository = repository;

    public async Task<Result<IEnumerable<CategoryOutput>>> Handle(GetCategoriesInput input)
    {
        var x = await _repository
            .FindCategoryById(input.ParentId ?? 0)
            .MatchAsync(
                c => _repository.GetAllCategoriesAsync(c), 
                e => _repository.GetAllCategoriesAsync());
            
        // Ugly workaround
        var y = await x;

        return y.Map(categories => categories
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
