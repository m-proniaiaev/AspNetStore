using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Services.Authorization.BlackList.Commands;

namespace Store.Core.Services.Authorization.Users.Commands
{
    public class LogoutCommand : IRequest
    {
    }
    
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IMediator _mediator;

        public LogoutCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddToBlackListCommand { }, cancellationToken);
            return Unit.Value;
        }
    }
}