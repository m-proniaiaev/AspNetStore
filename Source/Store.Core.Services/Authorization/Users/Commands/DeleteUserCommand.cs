using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.Users.Commands
{
    public class DeleteUserCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);

            if (user is null)
                throw new ArgumentException($"Can't get user {request.Id}");

            await _userService.DeleteUserAsync(user.Id, cancellationToken);
            
            return Unit.Value;
        }
    }
}