using MongoDB.Driver;
using Store.Core.Contracts.Models;

namespace Store.Core.Database.Database
{
    public interface IDbClient
    {
        public IMongoCollection<Record> GetRecordsCollection();
        public IMongoCollection<Seller> GetSellersCollection();
        public IMongoCollection<Role> GetRolesCollection();
        public IMongoCollection<User> GetUsersCollection();
        public IMongoCollection<BlackListRecord> GetBlackListCollection();
    }
}