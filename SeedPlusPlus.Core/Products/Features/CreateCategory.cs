using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Features;

public class CreateCategory : IUseCase<CreateCategoryInput, Result<CategoryOutput>>
{
    private readonly IProductRepository _repository;

    public CreateCategory(IProductRepository repository) => _repository = repository;

    public Task<Result<CategoryOutput>> Handle(CreateCategoryInput input)
    {
        return _repository.FindCategoryById(input.ParentId)
            .MapAsync(pc =>
                _repository.InsertCategoryAsync(new ProductCategory { Name = input.Name }, pc))
            .MapAsync(pc => new CategoryOutput(
                Id: pc.Id,
                Name: pc.Name,
                Left: pc.Left,
                Right: pc.Right,
                ParentId: pc.ParentId
                )
            );
    }
}

public record CreateCategoryInput(string Name, int ParentId);
// ParentId nullable in case it is a new root category

