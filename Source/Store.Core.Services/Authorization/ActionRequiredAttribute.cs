using System;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Services.Common.Interfaces;
using System.Linq;

namespace Store.Core.Services.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ActionRequiredAttribute : Attribute, IAuthorizationFilter
    {
        private const string ActionsAttribute = "actions";
        private string ActionName { get; }

        public ActionRequiredAttribute(string actionName)
        {
            ActionName = actionName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                return;

            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated || !CheckBlackList(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var permittedActions = user?.FindFirst(ActionsAttribute)?.Value
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (permittedActions != null && permittedActions.Contains(ActionName))
                return;

            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
        }
        
        
        private bool CheckBlackList(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var blackListCache = context.HttpContext.RequestServices
                .GetRequiredService<IBlackListService>();
            
            var blackListRecordTask = blackListCache.FindBlackList(Guid.Parse(userId), CancellationToken.None)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return blackListRecordTask == null;
        }
    }
}