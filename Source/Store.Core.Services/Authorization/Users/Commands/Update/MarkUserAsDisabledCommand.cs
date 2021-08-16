using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Authorization.BlackList.Commands;

namespace Store.Core.Services.Authorization.Users.Commands.Update
{
    public class MarkUserAsDisabledCommand : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class MarkUserAsDisabledCommandHandler : IRequestHandler<MarkUserAsDisabledCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;
        
        public MarkUserAsDisabledCommandHandler(IUserService userService, IMediator mediator, ICurrentUserService currentUser)
        {
            _userService = userService;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        
        public async Task<User> Handle(MarkUserAsDisabledCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);

            if (user is null)
                throw new ArgumentException($"Can't get user {request.Id}");

            await _userService.MarkUserAsDisabledAsync(user.Id, _currentUser.Id, cancellationToken);
            await _mediator.Send(new AddToBlackListCommand { Id = user.Id }, cancellationToken);
            
            var result = await _userService.GetUserAsync(user.Id, cancellationToken);
            
            if (result == null)
                throw new ArgumentException("Error updating entities!");

            return result;
        }
    }
}