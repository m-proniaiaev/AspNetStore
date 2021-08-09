using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.AuthHost.Services.Roles.Queries.CreateRole;

namespace Store.Core.Services.AuthHost.Services.Roles.Queries.UpdateRole
{
    public class UpdateRoleCommand : CreateRoleCommand, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role>
    {
        public async Task<Role> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}