using System;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Authorization.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid Role { get; set; }
    }
}