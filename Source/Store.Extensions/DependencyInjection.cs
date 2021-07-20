using Microsoft.AspNetCore.Builder;
using Store.Extensions.CorrelationIdMiddleware;
using Store.Extensions.Exceptions;

namespace Store.Extensions
{
    public static class DependencyInjection
    {
        public static IApplicationBuilder UseExtensions(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}