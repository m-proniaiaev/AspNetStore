using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Authorization.Roles.Queries.GetRoles;
using Store.Core.Services.Authorization.Users.Queries;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid Role { get; set; }
    }
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UpdateUserCommandHandler(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var nameIsTaken = (await _mediator.Send(new GetUsersQuery { Name = request.Name }, cancellationToken))
                .Users.Any();
            if (nameIsTaken)
            {
                throw new ArgumentException("User with such name already exists!");
            }
            
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = request.Role }, cancellationToken);

            if (role == null)
                throw new ArgumentException("There is no such role!");

            var user = await _userService.GetUserAsync(request.Id, cancellationToken);
            
            if(user is null)
                throw new ArgumentException("There is no such user !");

            user.Name = request.Name;
            user.IsActive = request.IsActive;
            user.Role = request.Role;

            await _userService.UpdateUserAsync(user, cancellationToken);
            
            var result = await _userService.GetUserAsync(request.Id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException("Can't create user!");

            return result;
        }
    }
}