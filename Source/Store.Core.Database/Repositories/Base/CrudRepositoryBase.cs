using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.Base
{
    public abstract class CrudRepositoryBase<TEntity> : ICrudRepository<TEntity>
        where TEntity : IIdentity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        protected CrudRepositoryBase(IMongoCollection<TEntity> collection)
        {
            _collection = collection;
        }

        public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
        }

        public virtual async Task<List<TEntity>> FindManyAsync(IDbQuery filter, CancellationToken cancellationToken)
        {
            var entities = await _collection.FindAsync(ConvertToFilterDefinition(filter),
                new FindOptions<TEntity>()
                {
                    Limit = filter.Limit,
                    Skip = filter.Skip
                },
                cancellationToken);
            
            return await entities.ToListAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(ConvertToFilterDefinition(entity), entity, new ReplaceOptions(), cancellationToken);
        }

        public virtual async Task DeleteAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(filter, cancellationToken);
        }
        
        protected abstract FilterDefinition<TEntity> ConvertToFilterDefinition(IDbQuery query);
        
        protected virtual FilterDefinition<TEntity> ConvertToFilterDefinition(TEntity entity)
        {
            return Builders<TEntity>.Filter.Eq(p => p.Id, entity.Id);
        }
    }
}