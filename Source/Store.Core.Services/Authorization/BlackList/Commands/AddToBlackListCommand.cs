using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;

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
            await _blackListService.AddToBlackList(new BlackListRecord { Id = request.Id, Permanent = request.Permanent }, cancellationToken);
            
            return Unit.Value;
        }
    }
}