using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Contracts.Responses;
using Store.Core.Host.Authorization.JWT;
using Store.Core.Services.Authorization.BlackList;
using Store.Core.Services.Authorization.BlackList.Commands;
using Store.Core.Services.Authorization.Roles.Queries.GetRoles;
using Store.Core.Services.Authorization.Users.Queries;

namespace Store.Core.Services.Authorization.Users.Commands
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IMediator _mediator;
        private readonly IHasher _hasher;
        private readonly IAuthManager _authManager;

        public LoginCommandHandler(IMediator mediator, IHasher hasher, IAuthManager authManager)
        {
            _mediator = mediator;
            _hasher = hasher;
            _authManager = authManager;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = (await _mediator.Send(new GetUsersQuery { Name = request.UserName }, cancellationToken))
                .Users.FirstOrDefault();

            if (user is null || !user.IsActive)
                throw new ArgumentException("Username or password is incorrect!");

            var validationResult = _hasher.CheckHash(user.Salt, user.Hash, request.Password);
            
            if (!validationResult)
                throw new ArgumentException("Username or password is incorrect!");

            var actions = (await _mediator.Send(new GetRoleByIdQuery { Id = user.Role }, cancellationToken)).Actions;
            
            var authClaims = new Claim[]
            {
                new(ClaimTypes.Name, request.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("actions", $"{string.Join(",", actions)}"),
                new("status", user.IsActive.ToString().ToLower())
            };

            var token = _authManager.GenerateToken(authClaims);

            var blackListed = await _mediator.Send(new GetBlackListQuery { Id = user.Id }, cancellationToken);

            if (blackListed != null)
                await _mediator.Send(new RemoveFromBlackListCommand { Id = user.Id }, cancellationToken);
            
            return new LoginResult()
            {
                Token = token,
                IsAuthenticated = true,
                Type = "Bearer"
            };
        }
    }
}