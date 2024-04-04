using System.Runtime.InteropServices;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Data.Repositories;

public class ProductRepositoryCLib : ProductRepository, IProductRepository
{
    public ProductRepositoryCLib(SeedPlusPlusContext context) : base(context)
    {
    }

    // includeAll has no effect
    async Task<Result<Product>> IProductRepository.FindByIdAsync(int id, bool includeAll)
    {
        // TODO: Store db path in config and inject here
        var path = Path.GetFullPath("Seeds.db");
        var productPtr = await LibC.FindProductById(path, id);
        
        if (productPtr == IntPtr.Zero)
            return new NotFoundException<Product>();  // TODO: It can actually be the result of any kind of error...
        
        var ps = Marshal.PtrToStructure<ProductStruct>(productPtr);
        
        LibC.FreeProduct(productPtr);

        return new Product
        {
            Id = ps.id,
            Name = ps.name,
            Price = (decimal)ps.price,
            TypeId = ps.type_id,
            CategoryId = ps.category_id,
            NumberInStock = ps.num_in_stock
        };
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct ProductStruct
{
    public int id;
    public int type_id;
    public int category_id;
    public int num_in_stock;
    public string name;
    public double price;
}
