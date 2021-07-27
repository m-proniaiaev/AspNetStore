using Microsoft.AspNetCore.Builder;
using Store.Core.Extensions.CorrelationIdMiddleware;
using Store.Core.Extensions.Exceptions;

namespace Store.Core.Extensions
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