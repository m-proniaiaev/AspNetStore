using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Contracts.Interfaces
{
    public interface ICacheService
    {
        Task<TRecord> GetCacheAsync<TRecord>(string id, CancellationToken cts = default);

        public Task<TRecord> GetCacheAsync<TRecord>(object[] keys, CancellationToken cancellationToken = default);

        Task AddCacheAsync<TRecord>(TRecord model, TimeSpan? expiration, CancellationToken cts = default)
            where TRecord : IIdentity;

        public Task AddCacheAsync<TRecord>(object[] keys, TRecord model, TimeSpan? expiration = default,
            CancellationToken cancellationToken = default);

        Task DeleteCacheAsync<TRecord>(string id, CancellationToken cts = default);
    }
}