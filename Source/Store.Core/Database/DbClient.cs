using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace Store.Core.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Record> _records;
        public DbClient(IOptions<RecordDbConfig> recordDbConfig)
        {
            var client = new MongoClient(recordDbConfig.Value.CONNECTION_STRING);
            var db = client.GetDatabase(recordDbConfig.Value.DbName);
            _records = db.GetCollection<Record>(recordDbConfig.Value.RecordCollectionName);
        }

        public IMongoCollection<Record> GetRecordsCollection() => _records;
        
    }
}