using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Store.Core.Contracts.Common;
using Store.Core.Host.Configurations;

namespace Store.Core.Host.Extensions.Exceptions
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string ErrorKeySelector(object obj) => (JObject.FromObject(obj)["Key"] ?? string.Empty).ToString();

                string ErrorValueSelector(ModelErrorCollection collection) =>
                    string.Join('|', collection.Select(arg => arg.ErrorMessage));

                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .ToDictionary(key => ErrorKeySelector(key),
                        value => ErrorValueSelector(value.Errors));

                var response = new ExceptionModel
                {
                    CorrelationId = context.HttpContext.Response.Headers[HostConstants.HttpCorrelationIdHeaderName],
                    Error = errors
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = 400
                };
            }
        }
    }
}

