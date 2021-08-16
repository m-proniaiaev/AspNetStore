using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Store.Core.Host.Extensions.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ArgumentException argumentException:
                    await ProcessExceptionMessage(context, argumentException, HttpStatusCode.BadRequest);
                    break;
                case ValidationException validationException:
                    await ProcessExceptionMessage(context, validationException, HttpStatusCode.BadRequest);
                    break;
                default:
                    await ProcessExceptionMessage(context, exception, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        private async Task ProcessExceptionMessage(HttpContext context, System.Exception exception, HttpStatusCode statusCode)
        {
            var model = new ExceptionModel
            {
                CorrelationId = context.Response.Headers[HostConstants.HttpCorrelationIdHeaderName],
                Error = exception.Message
            };
            
            //TODO add logger
            context.Response.Clear();
            context.Response.StatusCode = (int) statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(model));
        }
    }
}