using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Core.Contracts.Models;

namespace Store.Core.Database.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Record> _records;
        private readonly IMongoCollection<Seller> _sellers;
        public DbClient(IOptions<DbConfig> recordDbConfig)
        {
            if (recordDbConfig == null)
                throw new Exception("Can't configure MongoDb!");
            
            var client = new MongoClient(recordDbConfig.Value.ConnectionString);
            var db = client.GetDatabase(recordDbConfig.Value.DbName);
            _records = db.GetCollection<Record>(recordDbConfig.Value.RecordCollectionName);
            _sellers = db.GetCollection<Seller>(recordDbConfig.Value.SellerCollectionName);
        }

        public IMongoCollection<Record> GetRecordsCollection() => _records;
        public IMongoCollection<Seller> GetSellersCollection() => _sellers;

    }
}