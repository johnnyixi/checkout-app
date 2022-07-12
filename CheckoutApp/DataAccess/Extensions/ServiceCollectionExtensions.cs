using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Options;
using CheckoutApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutApp.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IBasketRepository, BasketRepository>();
    }

    public static void AddCheckoutDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        var checkoutDbContextOptions = configuration.GetSection(nameof(CheckoutDBContextOptions))?.Get<CheckoutDBContextOptions>();

        if (checkoutDbContextOptions?.UseInMemoryDb ?? false)
        {
            services.AddInMemoryCheckoutDbContext();
        }
        else
        {
            services.AddCheckoutDbContext(configuration.GetConnectionString("Default"));
        }
    }

    public static void AddCheckoutDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CheckoutContext>(contextBuilder
            => contextBuilder.UseSqlServer(connectionString));
    }

    public static void AddInMemoryCheckoutDbContext(this IServiceCollection services)
    {
        var dbName = $"{nameof(CheckoutContext)}_{DateTime.Now.ToFileTimeUtc()}";

        services.AddDbContext<CheckoutContext>(contextBuilder
            => contextBuilder.UseInMemoryDatabase(dbName));
    }
}
