using System;
using System.Collections.Generic;
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
        public IEnumerable<Record> GetRecords() => _records.Find(record => true).ToList();
        public Record AddRecord(Record record)
        {
            _records.InsertOne(record);
            return record;
        }
        public Record GetRecord(Guid id) => _records.Find(record => record.Id == id).FirstOrDefault();
        public void DeleteRecord(Guid id)
        {
            _records.DeleteOne(record => record.Id == id);
        }

        public Record UpdateRecord(Record record)
        {
            var currentRecord = GetRecord(record.Id);
            if (currentRecord == null) throw new Exception("No such record!");
            
            _records.ReplaceOne(r => r.Id == record.Id, record);
            return record;

        }

        public void MarkRecordAsSold(Guid id)
        {
            var currentRecord = GetRecord(id);
            if (currentRecord == null) throw new Exception("No such record!");
            if (currentRecord.IsSold)
                throw new Exception("This record already has been sold!");

            var update = Builders<Record>.Update
                .Set("IsSold", true)
                .Set("SoldDate", DateTime.Now);
            _records.UpdateOne(r => r.Id == id, update);
        }
    }
}