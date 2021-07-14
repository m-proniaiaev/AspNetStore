using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace Store.Core.Services
{
    public class RecordService : IRecordService
    {
        private readonly IMongoCollection<Record> _records;
        public RecordService(IDbClient client)
        {
            _records = client.GetRecordsCollection();
        }
        public List<Record> GetRecords() =>  _records.Find(record => true).ToList();
        public async Task<Record> AddRecord(Record record)
        {
            await _records.InsertOneAsync(record);
            return record;
        }
        public Record GetRecord(Guid id) => _records.Find(record => record.Id == id).FirstOrDefault();
        public async Task DeleteRecord(Guid id)
        {
           await _records.DeleteOneAsync(record => record.Id == id);
        }

        public async Task<Record> UpdateRecord(Record record)
        {
            var currentRecord = GetRecord(record.Id);
            if (currentRecord == null) throw new Exception("No such record!");
            
            await _records.ReplaceOneAsync(r => r.Id == record.Id, record);
            return record;
        }

        public async Task MarkRecordAsSold(Guid id)
        {
            var currentRecord = GetRecord(id);
            if (currentRecord == null) throw new Exception("No such record!");
            if (currentRecord.IsSold)
                throw new Exception("This record already has been sold!");

            var update = Builders<Record>.Update
                .Set("IsSold", true)
                .Set("SoldDate", DateTime.Now);
            await _records.UpdateOneAsync(r => r.Id == id, update);
        }
    }
}