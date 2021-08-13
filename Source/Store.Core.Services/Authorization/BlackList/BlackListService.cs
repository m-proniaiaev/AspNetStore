using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Authorization.BlackList.Commands;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.BlackList
{
    public class BlackListService : IBlackListService
    {
        private readonly ICacheService _cacheService;

        public BlackListService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task AddToBlackList(AddToBlackListCommand command, CancellationToken cancellationToken)
        {
            await _cacheService.AddCacheAsync(new BlackListRecord { Id = command.Id }, TimeSpan.FromHours(1),
                cancellationToken);
        }
        
        public async Task RemoveFromBlackList(RemoveFromBlackListCommand command, CancellationToken cancellationToken)
        {
            await _cacheService.DeleteCacheAsync<BlackListRecord>(command.Id.ToString(), cancellationToken);
        }

        public async Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken)
        {
            return await _cacheService.GetCacheAsync<BlackListRecord>(id.ToString(), cancellationToken);
        }
    }
}