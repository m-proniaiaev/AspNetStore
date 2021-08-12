using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.Roles.Queries.DisableRole
{
    public class DisableRoleCommand : IRequest<Role>, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DisableRoleCommandHandler : IRequestHandler<DisableRoleCommand, Role>
    {
        
        private readonly ICacheService _cacheService;
        private readonly IRoleService _roleService;

        public DisableRoleCommandHandler(ICacheService cacheService, IRoleService roleService)
        {
            _cacheService = cacheService;
            _roleService = roleService;
        }
        
        public async Task<Role> Handle(DisableRoleCommand request, CancellationToken cancellationToken)
        {
            var cachedRole = await _cacheService.GetCacheAsync<Role>(request.Id.ToString(), cancellationToken);

            var role = cachedRole ?? await _roleService.GetRoleAsync(request.Id, cancellationToken);
            
            if (role is null)
                throw new ArgumentException($"Can't find role {request.Id}");

            await _roleService.DisableRoleAsync(request.Id, cancellationToken);

            var result = await _roleService.GetRoleAsync(request.Id, cancellationToken);
            
            if (result is null)
                throw new InvalidOperationException($"Can't update role {request.Id}");

            await _cacheService.AddCacheAsync(request, TimeSpan.FromMinutes(15), cancellationToken);
            
            return result;
        }
    }
}