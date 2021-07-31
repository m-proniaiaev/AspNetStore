using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.Records.Queries.CreateRecord;
using Store.Core.Services.Records.Queries.UpdateRecord;

namespace Store.Core.Services.Records
{
    public class RecordService : IRecordService
    {
        private readonly IMongoCollection<Record> _records;
        public RecordService(IDbClient client)
        {
            _records = client.GetRecordsCollection();
        }
        public async Task<List<Record>> GetRecords(CancellationToken cancellationToken) 
            => await _records.Find(record => true).ToListAsync(cancellationToken);
        
        public async Task AddRecordAsync(CreateRecordCommand request, Guid id, CancellationToken cts)
        {
            var record = new Record
            {
                Id = id,
                Seller = request.Seller,
                Created = DateTime.Now,
                CreatedBy = Guid.Empty, //TODO change after adding users
                Name = request.Name,
                Price = request.Price,
                RecordType = request.RecordType,
                IsSold = false,
                SoldDate = null
            };
            
            await _records.InsertOneAsync(record, cancellationToken: cts);
        }
        public async Task<Record> GetRecord(Guid id, CancellationToken cancellationToken) 
            => await _records.Find(record => record.Id == id).FirstOrDefaultAsync(cancellationToken);
        
        public async Task DeleteRecord(Guid id, CancellationToken cts)
        {
           await _records.DeleteOneAsync(record => record.Id == id, cts);
        }

        public async Task<Record> UpdateRecord(UpdateRecordCommand request, Record origin, CancellationToken cts)
        {
            var record = new Record
            {
                Id = origin.Id,
                Seller = origin.Seller,
                Created = origin.Created,
                CreatedBy = origin.CreatedBy,
                RecordType = origin.RecordType,
                Name = request.Name,
                Price = request.Price,
                IsSold = request.IsSold,
                SoldDate = DateTime.Now
            };
            
            await _records.ReplaceOneAsync(r => r.Id == record.Id, record, cancellationToken: cts);
            return record;
        }

        public async Task MarkRecordAsSold(Guid id, CancellationToken cts)
        {
            var update = Builders<Record>.Update
                .Set("IsSold", true)
                .Set("SoldDate", DateTime.Now);
            await _records.UpdateOneAsync(r => r.Id == id, update, cancellationToken: cts);
        }
    }
}