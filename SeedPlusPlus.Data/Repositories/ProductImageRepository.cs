using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Data.Repositories;

public partial class ProductRepository : IProductRepository
{
    public async Task<Result<Image>> FindImageAsync(int id)
    {
        var result = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        return result is not null
            ? result
            : new NotFoundException<Image>();
    }
}