using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Data.Repositories;

public partial class ProductRepository : IProductRepository
{
    private readonly SeedPlusPlusContext _context;

    public ProductRepository(SeedPlusPlusContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Product>>> GetAllAsync()
    {
        try
        {
            return await _context.Products.ToArrayAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Result<IEnumerable<Product>>> GetAllFromCategoryAsync(ProductCategory parentCategory)
    {
        return await _context.Products
            .Join(
                _context.ProductCategories
                    .Where(parentCategory.InHierarchyIncludingParent()),
                product => product.CategoryId,
                category => category.Id,
                (product, category) => product)
            .ToArrayAsync();
    }

    public Task<Result<Product>> FindByIdAsync(int id, bool includeAll)
    {
        return includeAll
            ? FetchProductsIncludeAll(id)
            : FetchProducts(id);
    }

    private async Task<Result<Product>> FetchProductsIncludeAll(int id)
    {
        try
        {
            var product = await _context
                .Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return product is null 
                ? new NotFoundException<Product>() 
                : product;
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    private async Task<Result<Product>> FetchProducts(int id)
    {
        try
        {
            var product = await _context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return product is null 
                ? new NotFoundException<Product>() 
                : product;
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<Result<Product>> AddProductAsync(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product; 
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Result<Product>> UpdateProductAsync(Product product)
    {
        try
        {
            _context.Products.Update(product);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows == 1
                ? product
                : new NotFoundException<Product>();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Result<bool>> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        
        if (product is null)
            return new NotFoundException<Product>();

        try
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}
