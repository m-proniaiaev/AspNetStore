using MongoDB.Driver;
using Store.Core.Contracts.Domain;

namespace Store.Core.Database.Database
{
    public interface IDbClient
    {
        public IMongoCollection<Record> GetRecordsCollection();
        public IMongoCollection<Seller> GetSellersCollection();
        public IMongoCollection<Role> GetRolesCollection();
        public IMongoCollection<User> GetUsersCollection();
    }
}