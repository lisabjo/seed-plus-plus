using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Orders;

namespace SeedPlusPlus.Data;

public class OrderRepository : IOrderRepository
{
    public async Task<Order> FindById(int id)
    {
        return new Order{Id = id};
        // return await _context.Orders
        //            .FirstOrDefaultAsync(o => o.Id == id)
        //        ?? throw new NotFoundException();
    }
}