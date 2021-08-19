using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Requests;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Host.Authorization
{
    public class IdentityActionFilter : IActionFilter
    {
        private readonly ICurrentUserService _currentUser;

        public IdentityActionFilter(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var contextActionArgument in context.ActionArguments)
            {
                if (contextActionArgument.Value is IUserIdentityRequest userIdentityRequest)
                {
                    if (userIdentityRequest.Id == Guid.Empty)
                    {
                        userIdentityRequest.Id = _currentUser.Id;
                        return;
                    }

                    if (_currentUser.RoleType != RoleType.Administrator && _currentUser.Id != userIdentityRequest.Id)
                    {
                        throw new UnauthorizedAccessException("You are not allowed to view this data!");
                    }
                    
                    return;
                }
                
                if (contextActionArgument.Value is IRoleIdentityRequest roleIdentityRequest)
                {
                    if (roleIdentityRequest.Id == Guid.Empty)
                    {
                        roleIdentityRequest.Id = _currentUser.RoleId;
                        return;
                    }

                    if (_currentUser.RoleType != RoleType.Administrator && _currentUser.RoleId != roleIdentityRequest.Id)
                    {
                        throw new UnauthorizedAccessException("You are not allowed to view this data!");
                    }
                    
                    return;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}