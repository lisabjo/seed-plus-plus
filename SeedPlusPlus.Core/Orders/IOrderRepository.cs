using SeedPlusPlus.Core.Orders.Exceptions;

namespace SeedPlusPlus.Core.Orders;

public interface IOrderRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<Order> FindById(int id);
}