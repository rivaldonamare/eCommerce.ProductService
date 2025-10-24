namespace ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySQL(configuration.GetConnectionString("ProductServiceDB")!));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
