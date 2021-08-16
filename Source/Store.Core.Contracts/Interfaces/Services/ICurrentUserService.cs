using System;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
        bool IsActive { get; }
    }
}