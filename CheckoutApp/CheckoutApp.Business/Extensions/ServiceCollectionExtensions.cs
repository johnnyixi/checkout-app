using CheckoutApp.Business.Models;
using CheckoutApp.Business.Profiles;
using CheckoutApp.Business.Services;
using CheckoutApp.Business.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutApp.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCheckoutServices(this IServiceCollection services)
    {
        services.AddTransient<IBasketService, BasketService>();
        services.AddSingleton<IVatService, VatService>();
    }

    public static IServiceCollection AddModelValidators(this IServiceCollection services)
    {
        services.AddFluentValidation();

        services.AddTransient<IValidator<CreateBasketRequest>, CreateBasketRequestValidator>();
        services.AddTransient<IValidator<CreateArticleLineRequest>, CreateArticleLineRequestValidator>();

        return services;
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CheckoutProfile));

        return services;
    }

    public static IServiceCollection AddIdempotentApiServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddIdempotentAPIUsingDistributedCache();

        return services;
    }
}
