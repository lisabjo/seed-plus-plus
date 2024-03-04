namespace SeedPlusPlus.Core.Products;

public interface IProductCategoryRepository
{
    Task InsertProductCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory);
    Task<List<ProductCategory>> GetSubCategoriesAsync(ProductCategory category);
}