using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;

namespace Store.Core.Services.Records
{
    public class RecordService : IRecordService
    {
        private readonly IMongoCollection<Record> _records;
        public RecordService(IDbClient client)
        {
            _records = client.GetRecordsCollection();
        }
        public async Task<List<Record>> GetRecords() => await _records.Find(record => true).ToListAsync();
        public async Task<Record> AddRecord(Record record)
        {
            await _records.InsertOneAsync(record);
            return record;
        }
        public async Task<Record> GetRecord(Guid id) => await _records.Find(record => record.Id == id).FirstOrDefaultAsync();
        public async Task DeleteRecord(Guid id)
        {
           await _records.DeleteOneAsync(record => record.Id == id);
        }

        public async Task<Record> UpdateRecord(Record record)
        {
            await _records.ReplaceOneAsync(r => r.Id == record.Id, record);
            return record;
        }

        public async Task MarkRecordAsSold(Guid id)
        {
            var update = Builders<Record>.Update
                .Set("IsSold", true)
                .Set("SoldDate", DateTime.Now);
            await _records.UpdateOneAsync(r => r.Id == id, update);
        }
    }
}