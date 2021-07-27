using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Core.Contracts.Models;

namespace Store.Core.Database.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Record> _records;
        public DbClient(IOptions<DbConfig> recordDbConfig)
        {
            var client = new MongoClient(recordDbConfig.Value.ConnectionString);
            var db = client.GetDatabase(recordDbConfig.Value.DbName);
            _records = db.GetCollection<Record>(recordDbConfig.Value.RecordCollectionName);
        }

        public IMongoCollection<Record> GetRecordsCollection() => _records;
        
    }
}