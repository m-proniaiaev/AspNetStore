using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.BlackList
{
    public class GetBlackListQuery : IRequest<BlackListRecord>, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class GetBlackListQueryHandler : IRequestHandler<GetBlackListQuery, BlackListRecord>
    {
        private readonly ICacheService _cacheService;

        public GetBlackListQueryHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        
        public async Task<BlackListRecord> Handle(GetBlackListQuery request, CancellationToken cancellationToken)
        {
            return await _cacheService.GetCacheAsync<BlackListRecord>(request.Id.ToString(), cancellationToken);
        }
    }
}