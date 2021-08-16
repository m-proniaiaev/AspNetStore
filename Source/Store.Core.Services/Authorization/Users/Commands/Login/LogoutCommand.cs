using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Services.Authorization.BlackList.Commands;

namespace Store.Core.Services.Authorization.Users.Commands.Login
{
    public class LogoutCommand : IRequest
    {
    }
    
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public LogoutCommandHandler(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddToBlackListCommand { Id = _currentUserService.Id }, cancellationToken);
            return Unit.Value;
        }
    }
}