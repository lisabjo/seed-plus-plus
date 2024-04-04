using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeedPlusPlus.Core.Products.Contracts;
using SeedPlusPlus.Core.Tags;
using SeedPlusPlus.Data.Repositories;

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
        serviceCollection.AddScoped<ITagRepository, TagRepository>();
        
        return serviceCollection;
    }
}