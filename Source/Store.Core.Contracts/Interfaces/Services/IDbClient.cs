using MongoDB.Driver;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IDbClient
    {
        public IMongoCollection<Record> GetRecordsCollection();
        public IMongoCollection<Seller> GetSellersCollection();
        public IMongoCollection<Role> GetRolesCollection();
        public IMongoCollection<User> GetUsersCollection();
    }
}