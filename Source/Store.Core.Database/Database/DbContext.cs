using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Database.Database
{
    public class DbContext : IDbContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly DbConfig _dbConfig;

        public DbContext(IOptions<DbConfig> recordDbConfig, IMongoClient mongoClient)
        {
            if (recordDbConfig == null)
                throw new ArgumentException("Can't configure MongoDb!");

            _dbConfig = recordDbConfig.Value;
            _mongoClient = mongoClient;
        }

        public IMongoCollection<Record> RecordsCollection => GetMongoCollection<Record>(_dbConfig.RecordCollectionName);
        public IMongoCollection<Seller> SellersCollection => GetMongoCollection<Seller>(_dbConfig.SellerCollectionName);
        public IMongoCollection<Role> RolesCollection => GetMongoCollection<Role>(_dbConfig.RolesCollectionName);
        public IMongoCollection<User> UsersCollection => GetMongoCollection<User>(_dbConfig.UserCollectionName);
        
        
        private IMongoCollection<T> GetMongoCollection<T>(string collectionName)
        {
            var db = _mongoClient.GetDatabase(_dbConfig.DbName);
            return db.GetCollection<T>(collectionName);
        }
    }
}