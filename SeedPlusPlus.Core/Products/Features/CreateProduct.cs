using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;
using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Core.Products.Features;

public class CreateProduct : IUseCase<CreateProductInput, Result<CreateProductOutput>>
{
    private readonly IProductRepository _productRepository;
    private readonly ITagRepository _tagRepository;

    public CreateProduct(
        IProductRepository productRepository,
        ITagRepository tagRepository
        )
    {
        _productRepository = productRepository;
        _tagRepository = tagRepository;
    }
    
    public async Task<Result<CreateProductOutput>> Handle(CreateProductInput input)
    {
        var productTags = await CreateTags(input.Tags);
        var productImages = await CreateImages(input.Images);
        
        var product = new Product
        {
            Name = input.Name,
            Price = input.Price,
            TypeId = input.TypeId,
            CategoryId = input.CategoryId,
            ProductTags = productTags,
            ProductImages = productImages
        };

        return await _productRepository
            .AddProductAsync(product)
            .MapAsync(ToCreateProductOutput);
    }

    private async Task<List<ProductTag>> CreateTags(IEnumerable<ProductTagInput> tags)
    {
        var tasks = tags
            .Select(input => _tagRepository
                .FindById(input.TagId)
                .MapAsync(t => new ProductTag
                {
                    Tag = t,
                    Value = input.Value
                })
            ).ToList();

        var productTags = Result<ProductTag>.FilterOutErrors(await Task.WhenAll(tasks)).ToList();

        // var numberOfErrors = tasks.Count - productTags.Count;

        return productTags;
    }
    private async Task<List<ProductImage>> CreateImages(IEnumerable<ProductImageInput> images)
    {
        var tasks = images
            .Select((input, idx) => _productRepository
                .FindImageAsync(input.ImageId)
                .MapAsync(image => new ProductImage
                {
                    Image = image,
                    Index = idx
                })
            ).ToList();

        var productImages = Result<ProductImage>.FilterOutErrors(await Task.WhenAll(tasks)).ToList();

        // var numberOfErrors = tasks.Count - productImages.Count;

        return productImages;
    }

    private static CreateProductOutput ToCreateProductOutput(Product p)
    {
        return new CreateProductOutput(
            Id: p.Id,
            Name: p.Name,
            Price: p.Price,
            TypeId: p.TypeId,
            CategoryId: p.CategoryId,
            NumberInStock: p.NumberInStock,
            Tags: p.ProductTags.Select(pt => new ProductTagOutput(pt)).ToArray(),
            Images: p.ProductImages.Select(Helpers.ToProductImageOutput).ToArray()
        );
    }
}

public record CreateProductInput(string Name, decimal Price, int TypeId, int CategoryId,
    ProductImageInput[] Images, ProductTagInput[] Tags);

public record CreateProductOutput(int Id, string Name, decimal Price, int TypeId, int CategoryId, int NumberInStock,
    ProductImageOutput[] Images, ProductTagOutput[] Tags);
public record ProductTagInput(int TagId, byte[] Value);
public record ProductImageInput(int ImageId);
