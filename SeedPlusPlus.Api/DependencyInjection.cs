using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Products.Features;
using SeedPlusPlus.Core.Tags.Features;

namespace SeedPlusPlus.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .RegisterProductHandlers()
            .RegisterTagHandlers();
    }
    
    private static IServiceCollection RegisterProductHandlers(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IUseCase<GetProductByIdInput, Result<GetProductOutput>>, GetProductById>()
            .AddScoped<IUseCase<GetProductsInput, Result<IEnumerable<GetProductsOutput>>>, GetProducts>()
            .AddScoped<IUseCase<CreateProductInput, Result<CreateProductOutput>>, CreateProduct>()
            .AddScoped<IUseCase<AddImageInput, Result<bool>>, AddImage>()
            .AddScoped<IUseCase<UpdateProductInput, Result<UpdateProductOutput>>, UpdateProduct>()
            .AddScoped<IUseCase<DeleteProductInput, Result<bool>>, DeleteProduct>()
            .AddScoped<IUseCase<GetCategoriesInput, Result<IEnumerable<CategoryOutput>>>, GetCategories>()
            .AddScoped<IUseCase<CreateCategoryInput, Result<CategoryOutput>>, CreateCategory>();
    }
    
    private static IServiceCollection RegisterTagHandlers(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IUseCase<CreateTagInput, Result<CreateTagOutput>>, CreateTag>()
            .AddScoped<IUseCase<GetAllTagsInput, Result<GetAllTagsOutput>>, GetTags>()
            .AddScoped<IUseCase<GetTagInput, Result<GetTagOutput>>, GetTagById>()
            .AddScoped<IUseCase<DeleteTagInput, Result<bool>>, DeleteTag>();
    }
}
