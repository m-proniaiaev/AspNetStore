using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.Authorization.BlackList.Commands;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.BlackList
{
    public class BlackListService : IBlackListService
    {
        private readonly ICacheService _cacheService;
        private readonly IMongoCollection<BlackListRecord> _blackList;

        public BlackListService(ICacheService cacheService, IDbClient client)
        {
            _cacheService = cacheService;
            _blackList = client.GetBlackListCollection();
        }

        public async Task AddToBlackList(AddToBlackListCommand command, CancellationToken cancellationToken)
        {
            var record = new BlackListRecord { Id = command.Id };
            await _cacheService.AddCacheAsync(record, TimeSpan.FromHours(1),
                cancellationToken);
            
            if (command.Block)
            {
                await _blackList.InsertOneAsync(record, cancellationToken: cancellationToken);
            }
        }
        
        public async Task RemoveFromBlackList(RemoveFromBlackListCommand command, CancellationToken cancellationToken)
        {
            await _cacheService.DeleteCacheAsync<BlackListRecord>(command.Id.ToString(), cancellationToken);
            
            if (command.Unblock)
            {
                var blackListEntry = await FindBlackList(command.Id, cancellationToken);
                
                if (blackListEntry == null)
                {
                    return;
                }
                
                await _blackList.DeleteOneAsync(list => list.Id == command.Id, cancellationToken);
            }
        }

        public async Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken)
        {
            return await _blackList.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}