using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Internal.Records
{
    public class RecordService : IRecordService
    {
        private readonly IMongoCollection<Record> _records;
        public RecordService(IDbClient client)
        {
            _records = client.GetRecordsCollection();
        }
        public async Task<List<Record>> GetRecordsAsync(CancellationToken cancellationToken) 
            => await _records.Find(record => true).ToListAsync(cancellationToken);
        
        public async Task AddRecordAsync(Record record, CancellationToken cts)
        {
            await _records.InsertOneAsync(record, cancellationToken: cts);
        }
        public async Task<Record> GetRecordAsync(Guid id, CancellationToken cancellationToken) 
            => await _records.Find(record => record.Id == id).FirstOrDefaultAsync(cancellationToken);
        
        public async Task DeleteRecordAsync(Guid id, CancellationToken cts)
        {
           await _records.DeleteOneAsync(record => record.Id == id, cts);
        }

        public async Task UpdateRecordAsync(Record record, CancellationToken cts)
        {
            await _records.ReplaceOneAsync(r => r.Id == record.Id, record, cancellationToken: cts);
        }

        public async Task MarkRecordAsSoldAsync(Guid id, Guid editor, CancellationToken cts)
        {
            var update = Builders<Record>.Update
                .Set(x => x.IsSold, true)
                .Set(x => x.SoldDate, DateTime.Now)
                .Set(x => x.Edited, DateTime.Now)
                .Set(x => x.EditedBy, editor);
            
            await _records.UpdateOneAsync(r => r.Id == id, update, cancellationToken: cts);
        }
    }
}