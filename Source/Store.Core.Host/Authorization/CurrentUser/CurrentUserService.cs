using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Host.Authorization.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Guid Id => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                string.Empty, out var id) ? id : Guid.Empty;

        public bool IsActive => bool.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("status") ?? "false");

        public RoleType RoleType =>
            (RoleType)int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("role") ?? RoleType.Undefined.ToString());

        public Guid RoleId => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("roleId") ??
                                            string.Empty, out var id) ? id : Guid.Empty;
    }
}