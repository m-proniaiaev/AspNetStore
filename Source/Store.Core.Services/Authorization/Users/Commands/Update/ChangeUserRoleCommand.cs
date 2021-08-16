using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Authorization.Roles.Queries.GetRoles;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.Users.Commands.Update
{
    public class ChangeUserRoleCommand : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
        public Guid Role { get; set; }
    }
    
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public ChangeUserRoleCommandHandler(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<User> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);
            
            if (user == null)
                throw new ArgumentException("There is no such user!");
            
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = request.Role }, cancellationToken);

            if (role == null)
                throw new ArgumentException("There is no such role!");

            user.Role = request.Role;

            await _userService.ChangeUserRole(user, cancellationToken);
            
            var result = await _userService.GetUserAsync(user.Id, cancellationToken);
            
            if (result == null)
                throw new ArgumentException("Error updating entities!");

            return result;
        }
    }
}