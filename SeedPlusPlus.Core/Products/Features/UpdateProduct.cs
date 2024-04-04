using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Features;

// TODO: Include images and tags
public class UpdateProduct : IUseCase<UpdateProductInput, Result<UpdateProductOutput>>
{
    private readonly IProductRepository _repository;

    public UpdateProduct(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<UpdateProductOutput>> Handle(UpdateProductInput input)
    {
        return await _repository
            .FindByIdAsync(input.Id, false)
            .MapAsync(p => Hydrate(p, input))
            .MapAsync(_repository.UpdateProductAsync)
            .MapAsync(p => new UpdateProductOutput(
                Id: p.Id,
                Name: p.Name,
                Price: p.Price,
                TypeId: p.TypeId,
                CategoryId: p.CategoryId,
                NumberInStock: p.NumberInStock
            ));
    }

    private Product Hydrate(Product p, UpdateProductInput input)
    {
        p.Name = input.Name;
        p.Price = input.Price;
        p.TypeId = input.TypeId;
        p.CategoryId = input.CategoryId;
        return p;
    }
}

public record UpdateProductInput(int Id, string Name, decimal Price, int TypeId, int CategoryId);
public record UpdateProductOutput(int Id, string Name, decimal Price, int TypeId, int CategoryId, int NumberInStock);
