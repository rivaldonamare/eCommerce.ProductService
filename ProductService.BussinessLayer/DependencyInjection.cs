namespace ProductService.BussinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductToProductDTOMappingProfile).Assembly);
        services.AddScoped<IProductService, ProductService.BusinessLayer.Services.ProductService>();
        services.AddValidatorsFromAssemblyContaining<AddProductRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProductRequestValidator>();
        return services;
    }
}
