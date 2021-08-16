using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Store.Core.Host.Configurations;

namespace Store.Core.Host.Extensions.CorrelationIdMiddleware
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));
            
            if(httpContext.Request?.Headers != null)
            {
                httpContext.Request.Headers.TryGetValue(HostConstants.HttpCorrelationIdHeaderName,
                    out var correlationId);
                if(correlationId.Count > 0 ) CorrelationProcessor.SetCorrelationId(correlationId);
            }

            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.Headers[HostConstants.HttpCorrelationIdHeaderName] =
                    CorrelationProcessor.CorrelationId;
                httpContext.Response.Headers[HostConstants.HttpTimeSpan] =
                    DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            }

            using (LogContext.PushProperty(HostConstants.LogCorrelationId, CorrelationProcessor.CorrelationId))
                await _next(httpContext).ConfigureAwait(false);
        }
    }
}