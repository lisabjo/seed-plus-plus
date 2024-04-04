using SeedPlusPlus.Core.Products.Contracts;

namespace SeedPlusPlus.Core.Products.Features;

public class DeleteProduct : IUseCase<DeleteProductInput, Result<bool>>
{
    private readonly IProductRepository _repository;

    public DeleteProduct(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<bool>> Handle(DeleteProductInput input) 
        => await _repository.DeleteProductAsync(input.Id);
}

public record DeleteProductInput(int Id);
