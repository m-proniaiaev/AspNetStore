using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Store.Core.Contracts.Interfaces;

namespace Store.Core.Cache.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly TimeSpan _expiration;

        public CacheService(IDistributedCache distributedCache, IOptions<CacheOptions> options)
        {
            _distributedCache = distributedCache;
            _expiration = TimeSpan.FromMinutes(options.Value.ExpirationMinutes);
        }
        
        public async Task<TRecord> GetCacheAsync<TRecord>(string id, CancellationToken cts = default)
        {
            var cacheItem = await _distributedCache.GetStringAsync(GetRecordKey<TRecord>(id), cts);

            if (cacheItem == null)
                return default;

            return JsonConvert.DeserializeObject<TRecord>(cacheItem);
        }

        public Task AddCacheAsync<TRecord>(TRecord model, TimeSpan? expiration, CancellationToken cts = default) where TRecord : IIdentity
        {
            return AddAsyncImpl<TRecord>(model.Id.ToString(), model, default, cts);
        }

        public Task DeleteCacheAsync<TRecord>(string id, CancellationToken cts = default)
        {
            return _distributedCache.RemoveAsync(GetRecordKey<TRecord>(id), cts);
        }
        
        private static string GetRecordKey<TRecord>(string id)
        {
            return $"{typeof(TRecord).Name}-{id}";
        }
        
        private Task AddAsyncImpl<TRecord>(string id, TRecord record, TimeSpan? timeSpan, CancellationToken cts)
        {
            var expiration = timeSpan ?? _expiration;

            var serializedEntity = JsonConvert.SerializeObject(record, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(expiration);

            return _distributedCache.SetStringAsync(GetRecordKey<TRecord>(id), serializedEntity, options, cts);
        }
    }
}