using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Services.Authorization.Roles.Commands.CreateRole;

namespace Store.Core.Services.Authorization.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : CreateRoleCommand, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role>
    {
        private readonly ICacheService _cacheService;
        private readonly IRoleService _roleService;
        private readonly ICurrentUserService _currentUser;

        public UpdateRoleCommandHandler(ICacheService cacheService, IRoleService roleService, ICurrentUserService currentUser)
        {
            _cacheService = cacheService;
            _roleService = roleService;
            _currentUser = currentUser;
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
            role.Edited = DateTime.Now;
            role.EditedBy = _currentUser.Id;

            await _roleService.UpdateRoleAsync(role, cancellationToken);

            var result = await _roleService.GetRoleAsync(role.Id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException($"Can't update role {role.Id}");

            await _cacheService.AddCacheAsync(role, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}