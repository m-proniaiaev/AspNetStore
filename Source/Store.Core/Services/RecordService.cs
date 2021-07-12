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
        
    }
}