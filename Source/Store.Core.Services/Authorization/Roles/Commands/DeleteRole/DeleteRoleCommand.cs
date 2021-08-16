using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly ICacheService _cacheService;
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(ICacheService cacheService, IRoleService roleService)
        {
            _cacheService = cacheService;
            _roleService = roleService;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var cachedRole = await _cacheService.GetCacheAsync<Role>(request.Id.ToString(), cancellationToken);

            var role = cachedRole ?? await _roleService.GetRoleAsync(request.Id, cancellationToken);
            
            if (role is null)
                throw new ArgumentException($"Can't find role {request.Id}");

            await _roleService.DeleteRoleAsync(request.Id, cancellationToken);
            await _cacheService.DeleteCacheAsync<Role>(request.Id.ToString(), cancellationToken);
            
            return Unit.Value;
        }
    }
}