using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Common;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.BlackList
{
    public class GetBlackListQuery : IRequest<BlackListRecord>, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class GetBlackListQueryHandler : IRequestHandler<GetBlackListQuery, BlackListRecord>
    {
        private readonly IBlackListService _blackListService;

        public GetBlackListQueryHandler(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }
        
        public async Task<BlackListRecord> Handle(GetBlackListQuery request, CancellationToken cancellationToken)
        {
            return await _blackListService.FindBlackList(request.Id, cancellationToken);
        }
    }
}