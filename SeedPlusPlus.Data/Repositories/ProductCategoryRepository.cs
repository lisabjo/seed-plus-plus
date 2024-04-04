using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Data.Repositories;

public partial class ProductRepository : IProductRepository
{
    public async Task<Result<ProductCategory>> FindCategoryById(int id)
    {
        var result = await _context.ProductCategories.FindAsync(id);
        return result is not null ? result : new NotFoundException<ProductCategory>();
    }

    // TODO: The table should be locked until the update is completed
    public async Task<Result<ProductCategory>> InsertCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory)
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

        try
        {
            await _context.SaveChangesAsync();
            return newCategory;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Result<IEnumerable<ProductCategory>>> GetAllCategoriesAsync(ProductCategory? parentCategory = default)
    {
        var query = _context.ProductCategories.AsQueryable();
        if (parentCategory is not null)
            query = query.Where(parentCategory.InHierarchyIncludingParent());

        return await query.ToArrayAsync();
    }
}

internal static class Extensions
{
    public static Expression<Func<ProductCategory, bool>> InHierarchyIncludingParent(this ProductCategory parentCategory) =>
        c => c.Left >= parentCategory.Left && c.Right <= parentCategory.Right;
}