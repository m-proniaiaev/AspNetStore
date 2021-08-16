using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Cache.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly TimeSpan _expiration;

        public CacheService(IDistributedCache distributedCache, IOptions<CacheOptions> options)
        {
            if (options == null)
                throw new Exception("Can't configure redis cache!");
            
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

        public Task<TRecord> GetCacheAsync<TRecord>(object[] keys, CancellationToken cancellationToken = default)
        {
            return GetCacheAsync<TRecord>(GetKeys(keys), cancellationToken);
        }

        public Task AddCacheAsync<TRecord>(TRecord model, TimeSpan? expiration = default, CancellationToken cts = default) where TRecord : IIdentity
        {
            return AddAsyncImpl<TRecord>(model.Id.ToString(), model, expiration, cts);
        }

        public Task AddCacheAsync<TRecord>(object[] keys, TRecord model, TimeSpan? expiration = default,
            CancellationToken cancellationToken = default)
        {
            return AddAsyncImpl<TRecord>(GetKeys(keys), model, expiration, cancellationToken);
        }

        public Task DeleteCacheAsync<TRecord>(string id, CancellationToken cts = default)
        {
            return _distributedCache.RemoveAsync(GetRecordKey<TRecord>(id), cts);
        }
        
        private static string GetRecordKey<TRecord>(string id)
        {
            return $"{typeof(TRecord).Name}-{id}";
        }
        
        private static string GetKeys(object[] keys)
        {
            var strKeys = keys?.Select(x => x.ToString()).ToArray();

            return string.Join('$', strKeys ?? Array.Empty<string>());
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