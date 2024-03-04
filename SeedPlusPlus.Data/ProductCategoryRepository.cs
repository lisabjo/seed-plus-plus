using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Data;

public partial class ProductRepository : IProductCategoryRepository
{
    // TODO: The table should be locked until the update is completed
    public async Task InsertProductCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory)
    {
        var leftId = parentCategory.Right;
        var rightId = parentCategory.Right + 1;

        // Update existing nodes
        var affectedCategories =
            _context.ProductCategories
                .Where(c => c.Left >= leftId || c.Right >= leftId);
        foreach (var category in affectedCategories)
        {
            if (category.Left >= leftId) category.Left += 2;
            if (category.Right >= leftId) category.Right += 2;
        }

        // Insert new node
        newCategory.Left = leftId;
        newCategory.Right = rightId;
        newCategory.Parent = parentCategory;

        _context.ProductCategories.Add(newCategory);
        
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductCategory>> GetSubCategoriesAsync(ProductCategory category)
    {
        return await _context.ProductCategories
            .Where(c => c.Left > category.Left && c.Right < category.Right)
            .ToListAsync();
    }
}
