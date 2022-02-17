using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.Base
{
    public interface ICrudRepository<TEntity> 
        where TEntity : IIdentity
    {
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<List<TEntity>> FindManyAsync(IDbQuery filter, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken);
    }
}