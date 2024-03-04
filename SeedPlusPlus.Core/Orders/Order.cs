namespace SeedPlusPlus.Core.Orders;

public class Order
{
    public int Id { get; set; } // Equivalent to order number?
    public DateTime CreatedAt { get; set; }
    public OrderDetails OrderDetails { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}