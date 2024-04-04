namespace SeedPlusPlus.Core.Products.Entities;

public class Batch
{
    public int Id { get; set; }
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.MinValue);  // Or Today
    public DateOnly ExpiresAt { get; set; } = DateOnly.FromDateTime(DateTime.MaxValue);
}