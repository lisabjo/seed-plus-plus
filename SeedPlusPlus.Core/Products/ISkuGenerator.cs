namespace SeedPlusPlus.Core.Products;

public interface ISkuGenerator
{
    // TODO: Should depend on the Product and on other things such as batch or color
    public Task<string> GenerateSkuAsync(Product product);
}

public class BasicSkuGenerator : ISkuGenerator
{
    public Task<string> GenerateSkuAsync(Product product)
    {
        throw new NotImplementedException();
    }
}