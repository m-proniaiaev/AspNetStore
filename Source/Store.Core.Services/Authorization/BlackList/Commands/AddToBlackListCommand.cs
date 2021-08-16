using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Common;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.BlackList.Commands
{
    public class AddToBlackListCommand : BlackListRecord, IRequest
    {
    }
    
    public class AddToBlackListCommandHandler : IRequestHandler<AddToBlackListCommand>
    {
        private readonly IBlackListService _blackListService;

        public AddToBlackListCommandHandler(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }

        public async Task<Unit> Handle(AddToBlackListCommand request, CancellationToken cancellationToken)
        {
            await _blackListService.AddToBlackList(request.Id, cancellationToken);
            
            return Unit.Value;
        }
    }
}