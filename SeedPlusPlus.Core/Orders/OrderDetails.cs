namespace SeedPlusPlus.Core.Orders;

public class OrderDetails
{
    public int Id { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Tax { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string BillingAddress { get; set; }
    public string ShippingAddress { get; set; }
    public string Message { get; set; }
}

// order status: processing, confirmed, backordered, shipped, completed, canceled ...
// discount