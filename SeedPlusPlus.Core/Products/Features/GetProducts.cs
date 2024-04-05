using SeedPlusPlus.Core.Products.Contracts;

namespace SeedPlusPlus.Core.Products.Features;

public class GetProducts : IUseCase<GetProductsInput, Result<IEnumerable<GetProductsOutput>>>
{
    private readonly IProductRepository _repository;

    public GetProducts(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<IEnumerable<GetProductsOutput>>> Handle(GetProductsInput input)
    {
        if (!input.CategoryId.HasValue)
        {
            return await _repository.GetAllAsync()
                .MapAsync(ps => ps.Select(
                    p => new GetProductsOutput(p.Id, p.Name, p.Price, p.TypeId, p.CategoryId, p.NumberInStock)));
        }

        return await _repository.FindCategoryById((int)input.CategoryId!)
            .MapAsync(pc => _repository.GetAllFromCategoryAsync(pc))
            .MapAsync(ps => ps.Select(
                p => new GetProductsOutput(p.Id, p.Name, p.Price, p.TypeId, p.CategoryId, p.NumberInStock)));
    }
}

// TODO: Map in the API instead, keep the Products as they are here and return them in the output
public record GetProductsInput(int? CategoryId);
public record GetProductsOutput(int Id, string Name, decimal Price, int TypeId, int CategoryId, int NumberInStock);
