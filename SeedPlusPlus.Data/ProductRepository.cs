using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Exceptions;
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

    
    public async Task<Product> FindById(int id)
    {
        var path = Path.GetFullPath("Seeds.db");
        IntPtr productPtr = LibC.FindProductById(path, id);
        
        if (productPtr != IntPtr.Zero)
        {
            // Marshal the pointer to a C# struct
            ProductStruct product = Marshal.PtrToStructure<ProductStruct>(productPtr);

            // Access fields of the product struct
            Console.WriteLine($"Product ID: {product.id}");
            Console.WriteLine($"Product name: {product.name}");

            // Free the allocated memory in C
            LibC.FreeProduct(productPtr);
        }

        return new Product();

        // return await _context.Products.FirstOrDefaultAsync(p => p.Id == id)
        //     ?? throw new NotFoundException<Product>();
    }

    public Task<Product> AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsInStock(Product product)
    {
        return await _context.Products
            .AnyAsync(p => p.Id == product.Id
                           && p.Skus.Any(sku => sku.Quantity > 0));
    }

    public async Task<ProductCategory> FindCategoryById(int id)
    {
        return await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id)
               ?? throw new NotFoundException<ProductCategory>();
    }

    // TODO: The table should be locked until the update is completed
    public async Task InsertProductCategoryAsync(ProductCategory newCategory, ProductCategory parentCategory)
    {
        LibC.InsertIt();
        
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

[StructLayout(LayoutKind.Sequential)]
public struct ProductStruct
{
    public int id;
    public string name;
}


internal static class Extensions
{
    public static Expression<Func<ProductCategory, bool>> HierarchyIncludingParent(this ProductCategory parentCategory) =>
        c => c.Left >= parentCategory.Left && c.Right <= parentCategory.Right;
}