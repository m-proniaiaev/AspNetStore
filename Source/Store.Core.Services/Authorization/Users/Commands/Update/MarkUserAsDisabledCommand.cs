using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Authorization.BlackList.Commands;
using Store.Core.Services.Common.Interfaces;

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

        public MarkUserAsDisabledCommandHandler(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }
        
        public async Task<User> Handle(MarkUserAsDisabledCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);

            if (user is null)
                throw new ArgumentException($"Can't get user {request.Id}");

            await _userService.MarkUserAsDisabledAsync(user.Id, cancellationToken);
            await _mediator.Send(new AddToBlackListCommand { Id = user.Id }, cancellationToken);
            
            var result = await _userService.GetUserAsync(user.Id, cancellationToken);
            
            if (result == null)
                throw new ArgumentException("Error updating entities!");

            return result;
        }
    }
}