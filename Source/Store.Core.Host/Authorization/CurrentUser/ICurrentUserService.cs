using System;

namespace Store.Core.Host.Authorization.CurrentUser
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
        bool IsActive { get; }
    }
}