using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Authorization.Roles.Queries.GetActions;

namespace Store.Core.Services.Authorization.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Role>
    {
        public string Name { get; set; }
        public string[] Actions { get; set; }
        public RoleType RoleType { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role>
    {
        private readonly IRoleService _roleService;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public CreateRoleCommandHandler(IRoleService roleService, IMediator mediator, ICurrentUserService currentUser)
        {
            _roleService = roleService;
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var allowedRoleActions =
                (await _mediator.Send(new GetActionsQuery { RoleType = request.RoleType }, cancellationToken)).Actions;

            var actions = ValidateActions(allowedRoleActions, request.Actions);

            var id = Guid.NewGuid();
            
            var role = new Role()
            {
                Id = id,
                Name = request.Name,
                IsActive = request.IsActive,
                RoleType = request.RoleType,
                Actions = actions,
                Created = DateTime.Now,
                CreatedBy = _currentUser.Id
            };
            
            await _roleService.CreateRoleAsync(role, cancellationToken);

            var result = await _roleService.GetRoleAsync(id, cancellationToken);

            if (result == null)
                throw new InvalidOperationException("Role was not created!");

            return result;
        }

        private static string[] ValidateActions(string[] allowedActions, string[] actions)
        {
            if (actions?.Length <= 0) return allowedActions;
            
            var createdActions = allowedActions.Intersect(actions).ToArray();

            return createdActions.Any()
                ? createdActions
                : throw new ArgumentException("Provided actions is out of this role scope!");

        }
    }
}