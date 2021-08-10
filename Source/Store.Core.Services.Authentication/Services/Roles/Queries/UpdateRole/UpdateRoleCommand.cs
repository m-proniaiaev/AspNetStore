using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.AuthHost.Common.Interfaces;
using Store.Core.Services.AuthHost.Services.Roles.Queries.CreateRole;

namespace Store.Core.Services.AuthHost.Services.Roles.Queries.UpdateRole
{
    public class UpdateRoleCommand : CreateRoleCommand, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role>
    {
        private readonly ICacheService _cacheService;
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(ICacheService cacheService, IRoleService roleService)
        {
            _cacheService = cacheService;
            _roleService = roleService;
        }
        public async Task<Role> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var cachedRole = await _cacheService.GetCacheAsync<Role>(request.Id.ToString(), cancellationToken);

            var role = cachedRole ?? await _roleService.GetRoleAsync(request.Id, cancellationToken);

            if (role is null)
                throw new ArgumentException($"Can't find role {request.Id}");
            
            role.Name = request.Name;
            role.RoleType = request.RoleType;
            role.IsActive = request.IsActive;
            role.Actions = request.Actions;

            await _roleService.UpdateRoleAsync(role, cancellationToken);

            var result = await _roleService.GetRoleAsync(role.Id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException($"Can't update role {role.Id}");

            await _cacheService.AddCacheAsync(role, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}