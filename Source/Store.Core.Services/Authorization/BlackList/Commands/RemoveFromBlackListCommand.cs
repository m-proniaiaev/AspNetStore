using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.BlackList.Commands
{
    public class RemoveFromBlackListCommand : BlackListRecord, IRequest
    {
    }
    
    public class RemoveFromBlackListCommandHandler : IRequestHandler<RemoveFromBlackListCommand>
    {
        private readonly IBlackListService _blackListService;

        public RemoveFromBlackListCommandHandler(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }

        public async Task<Unit> Handle(RemoveFromBlackListCommand request, CancellationToken cancellationToken)
        {
            await _blackListService.RemoveFromBlackList(request.Id, cancellationToken);
            
            return Unit.Value;
        }
    }
}