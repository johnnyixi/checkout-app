using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutApp.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IBasketRepository, BasketRepository>();
    }

    public static void AddCheckoutDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CheckoutContext>(contextBuilder
            => contextBuilder.UseSqlServer(connectionString));
    }
}
