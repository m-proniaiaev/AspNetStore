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
        private readonly IMongoCollection<Role> _roles;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<BlackListRecord> _blackList;

        public DbClient(IOptions<DbConfig> recordDbConfig)
        {
            if (recordDbConfig == null)
                throw new Exception("Can't configure MongoDb!");
            
            var client = new MongoClient(recordDbConfig.Value.ConnectionString);
            var db = client.GetDatabase(recordDbConfig.Value.DbName);
            
            if (db is null)
                throw new InvalidOperationException("Can't connect to db!");
            
            _records = db.GetCollection<Record>(recordDbConfig.Value.RecordCollectionName);
            _sellers = db.GetCollection<Seller>(recordDbConfig.Value.SellerCollectionName);
            _roles = db.GetCollection<Role>(recordDbConfig.Value.RolesCollectionName);
            _users = db.GetCollection<User>(recordDbConfig.Value.UserCollectionName);
            _blackList = db.GetCollection<BlackListRecord>(recordDbConfig.Value.BlackListCollectionName);
        }

        public IMongoCollection<Record> GetRecordsCollection() => _records;
        public IMongoCollection<Seller> GetSellersCollection() => _sellers;
        public IMongoCollection<Role> GetRolesCollection() => _roles;
        public IMongoCollection<User> GetUsersCollection() => _users;
        public IMongoCollection<BlackListRecord> GetBlackListCollection() => _blackList;
    }
}