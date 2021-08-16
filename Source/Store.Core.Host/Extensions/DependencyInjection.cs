using Microsoft.AspNetCore.Builder;
using Store.Core.Host.Extensions.CorrelationIdMiddleware;
using Store.Core.Host.Extensions.Exceptions;

namespace Store.Core.Host.Extensions
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