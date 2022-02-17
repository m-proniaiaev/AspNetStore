using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.RecordRepository
{
    public class RecordRepository : CrudRepositoryBase<Record>, IRecordRepository

    {
        public RecordRepository(IDbContext context) : base(context.RecordsCollection)
        {
        }

        protected override FilterDefinition<Record> ConvertToFilterDefinition(IDbQuery query)
        {
            if (query is not RecordFilter recordQuery)
                return FilterDefinition<Record>.Empty;
            
            var builder = Builders<Record>.Filter;

            var filter = builder.Empty;
            
            if (recordQuery.Id != Guid.Empty)
                filter = builder.And(filter, builder.Eq(x => x.Id, query.Id));

            if (recordQuery.IsSold.HasValue)
                filter = builder.And(filter, builder.Eq(r => r.IsSold, recordQuery.IsSold.Value));

            return filter;
        }

        public async Task SellAsync(Guid id, Guid editor, CancellationToken cts)
        {
            var update = Builders<Record>.Update
                .Set(x => x.IsSold, true)
                .Set(x => x.SoldDate, DateTime.Now)
                .Set(x => x.Edited, DateTime.Now)
                .Set(x => x.EditedBy, editor);

            var filter = Builders<Record>.Filter.Eq(r => r.Id, id);

            await _collection.UpdateOneAsync(filter, update, cancellationToken: cts);
        }
    }
}