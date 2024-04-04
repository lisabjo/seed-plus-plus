using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Features;

public class GetProductById : IUseCase<GetProductByIdInput, Result<GetProductOutput>>
{
    private readonly IProductRepository _repository;

    public GetProductById(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Result<GetProductOutput>> Handle(GetProductByIdInput input)
    {
        return _repository.FindByIdAsync(input.Id, input.IncludeAll)
            .MapAsync(p =>
                new GetProductOutput(
                    Id: p.Id,
                    Name: p.Name,
                    Price: p.Price,
                    TypeId: p.TypeId,
                    CategoryId: p.CategoryId,
                    NumberInStock: p.NumberInStock,
                    Tags: p.ProductTags.Select(pt => new ProductTagOutput(pt)).ToArray(),
                    Images: p.ProductImages.Select(Helpers.ToProductImageOutput).ToArray()  // TODO are they empty or null
                )
            );
    }
}

public record GetProductByIdInput(int Id, bool IncludeAll);
public record GetProductOutput(int Id, string Name, decimal Price, int TypeId, int CategoryId,
    int NumberInStock, ProductImageOutput[] Images, ProductTagOutput[] Tags);
public record ProductTagOutput(ProductTag ProductTag);
public record ProductImageOutput(int ImageId, string AltText, int Index, string ImageType);
