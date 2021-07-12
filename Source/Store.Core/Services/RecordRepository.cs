using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace Store.Core.Services
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IMongoCollection<Record> _records;
        public RecordRepository(IDbClient client)
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
    }
}