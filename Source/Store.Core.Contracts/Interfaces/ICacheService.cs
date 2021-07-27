using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Contracts.Interfaces
{
    public interface ICacheService
    {
        Task<TRecord> GetCacheAsync<TRecord>(string id, CancellationToken cts = default);

        Task AddCacheAsync<TRecord>(TRecord model, TimeSpan? expiration, CancellationToken cts = default)
            where TRecord : IIdentity;

        Task DeleteCacheAsync<TRecord>(string id, CancellationToken cts = default);
    }
}