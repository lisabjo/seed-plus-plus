using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Products;

namespace SeedPlusPlus.Data;

public partial class ProductRepository : IProductRepository
{
    private readonly SeedPlusPlusContext _context;

    public ProductRepository(SeedPlusPlusContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetAllFromCategoryAsync(ProductCategory parentCategory)
    {
        return await _context.Products
            .Join(
                _context.ProductCategories
                    .Where(parentCategory.HierarchyIncludingParent()),
                product => product.CategoryId,
                category => category.Id,
                (product, category) => product)
            .ToListAsync();
    }
}

internal static class Extensions
{
    public static Expression<Func<ProductCategory, bool>> HierarchyIncludingParent(this ProductCategory parentCategory) =>
        c => c.Left >= parentCategory.Left && c.Right <= parentCategory.Right;
}