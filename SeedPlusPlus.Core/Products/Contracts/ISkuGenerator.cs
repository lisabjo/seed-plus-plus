using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Contracts;

public interface ISkuGenerator
{
    public Task<string> GenerateSkuAsync(StockKeepingUnit sku);
}

public class BasicSkuGenerator : ISkuGenerator
{
    private int _index; // Needs to be persisted
    
    public Task<string> GenerateSkuAsync(StockKeepingUnit sku)
    {
        
        
        throw new NotImplementedException();
    }
}
