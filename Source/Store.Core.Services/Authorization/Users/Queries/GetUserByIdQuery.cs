using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class UserGetByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserService _userService;

        public UserGetByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);

            if (user is null)
                throw new ArgumentException($"Can't get user {request.Id}");

            return user;
        }
    }
}