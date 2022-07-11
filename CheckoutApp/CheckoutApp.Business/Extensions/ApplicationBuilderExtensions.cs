using CheckoutApp.Business.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CheckoutApp.Business.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
