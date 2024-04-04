namespace SeedPlusPlus.Core.Products.Entities;

public class StockKeepingUnit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public Batch? Batch { get; set; }
    public int Quantity { get; set; }
}
