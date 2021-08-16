using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Common;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Authorization.BlackList
{
    public class BlackListService : IBlackListService
    {
        private readonly ICacheService _cacheService;

        public BlackListService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task AddToBlackList(Guid id, CancellationToken cancellationToken)
        {
            await _cacheService.AddCacheAsync(new BlackListRecord { Id = id }, TimeSpan.FromHours(1),
                cancellationToken);
        }
        
        public async Task RemoveFromBlackList(Guid id, CancellationToken cancellationToken)
        {
            await _cacheService.DeleteCacheAsync<BlackListRecord>(id.ToString(), cancellationToken);
        }

        public async Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken)
        {
            return await _cacheService.GetCacheAsync<BlackListRecord>(id.ToString(), cancellationToken);
        }
    }
}