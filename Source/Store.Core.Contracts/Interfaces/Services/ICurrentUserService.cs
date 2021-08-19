using System;
using Store.Core.Contracts.Enums;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
        bool IsActive { get; }
        public RoleType RoleType { get; }

        public Guid RoleId { get; }
    }
}