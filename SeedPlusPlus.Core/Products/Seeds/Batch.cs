namespace SeedPlusPlus.Core.Products.Seeds;

public class Batch
{
    public int Id { get; set; }
    public Variety Variety { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly ExpiresAt { get; set; }
}