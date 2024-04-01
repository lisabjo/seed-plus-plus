namespace SeedPlusPlus.Core.Products.Features;

public class CreateProduct : IUseCase<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;

    public CreateProduct(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<CreateProductResponse> Handle(CreateProductRequest input)
    {
        var category = await _productRepository.FindCategoryById(input.CategoryId);
        
        var product = new Product
        {
            Name = input.Name,
            Price = input.Price,
            CategoryId = input.CategoryId
        };
        
        
        
        throw new NotImplementedException();
    }
}

public record CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public record CreateProductResponse(Product Product);