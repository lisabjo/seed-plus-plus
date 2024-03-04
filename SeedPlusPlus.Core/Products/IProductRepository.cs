namespace SeedPlusPlus.Core.Products;

public interface IProductRepository
{
    Task<List<Product>> GetAllFromCategoryAsync(ProductCategory category);
    
}