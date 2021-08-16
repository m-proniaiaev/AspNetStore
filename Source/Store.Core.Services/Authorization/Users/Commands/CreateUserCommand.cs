using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Authorization.Roles.Queries.GetRoles;
using Store.Core.Services.Authorization.Users.Queries;

namespace Store.Core.Services.Authorization.Users.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public Guid Role { get; set; }   
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IHasher _hasher;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public CreateUserCommandHandler(IUserService userService, IHasher hasher, IMediator mediator, ICurrentUserService currentUser)
        {
            _userService = userService;
            _hasher = hasher;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

            var password = _hasher.Hash(request.Password);

            if (password.salt == null || password.hash == null)
                throw new ArgumentException("Can't create user with provided password!");
                

            var id = Guid.NewGuid();
            var user = new User
            { 
                Id = id,
               Name = request.Name,
               IsActive = request.IsActive,
               Role = role.Id,
               Salt = password.salt,
               Hash = password.hash,
               Created = DateTime.Now,
               CreatedBy = _currentUser.Id
            };

            await _userService.AddUserAsync(user, cancellationToken);

            var result = await _userService.GetUserAsync(id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException("Can't create user!");

            return result;
        }
    }
}