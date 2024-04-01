using SeedPlusPlus.Core.Exceptions;

namespace SeedPlusPlus.Core.Products;

public interface IProductRepository
{
    Task<List<Product>> GetAllFromCategoryAsync(ProductCategory category);
    Task<Product> FindById(int id);
    Task<Product> AddProduct(Product product);
    Task<bool> IsInStock(Product product);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException{TEntity}"></exception>
    Task<ProductCategory> FindCategoryById(int id);
    Task InsertProductCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory);
    Task<List<ProductCategory>> GetSubCategoriesAsync(ProductCategory category);
}