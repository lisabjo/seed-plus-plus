using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Contracts;

// TODO: segregate into three separate interfaces?
public interface IProductRepository
{
    Task<Result<IEnumerable<Product>>> GetAllAsync();
    Task<Result<IEnumerable<Product>>> GetAllFromCategoryAsync(ProductCategory category);
    Task<Result<Product>> FindByIdAsync(int id, bool includeAll);
    Task<Result<Product>> AddProductAsync(Product product);
    Task<Result<Product>> UpdateProductAsync(Product product);
    Task<Result<bool>> DeleteProductAsync(int id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException{TEntity}"></exception>
    Task<Result<ProductCategory>> FindCategoryById(int id);
    Task<Result<ProductCategory>> InsertCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory);
    Task<Result<IEnumerable<ProductCategory>>> GetAllCategoriesAsync(ProductCategory? category = default);

    Task<Result<Image>> FindImageAsync(int id);
}
