using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Authorization.Services.Roles.Queries.GetRoles;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Authorization.Services.Users.Query
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

        public CreateUserCommandHandler(IUserService userService, IHasher hasher, IMediator mediator)
        {
            _userService = userService;
            _hasher = hasher;
            _mediator = mediator;
        }
        
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = request.Role }, cancellationToken);

            if (role == null)
                throw new ArgumentException("There is no such role!");

            var password = _hasher.Hash(request.Password);

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
               CreatedBy = Guid.Empty //TODO
            };

            await _userService.AddUserAsync(user, cancellationToken);

            var result = await _userService.GetUserAsync(id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException("Can't create user!");

            return result;
        }
    }
}