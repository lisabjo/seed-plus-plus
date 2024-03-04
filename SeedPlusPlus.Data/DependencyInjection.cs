using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core.Orders;
using SeedPlusPlus.Core.Products;
using Microsoft.Extensions.DependencyInjection;

namespace SeedPlusPlus.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddSqliteDbContext(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<SeedPlusPlusContext>(options => 
            options
                .UseSqlite(connectionString)
                .EnableSensitiveDataLogging());
        return serviceCollection;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        return serviceCollection;
    }
}