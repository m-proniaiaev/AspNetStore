using MongoDB.Driver;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IDbContext
    {
        public IMongoCollection<Record> RecordsCollection { get; }
        public IMongoCollection<Seller> SellersCollection { get; }
        public IMongoCollection<Role> RolesCollection { get; }
        public IMongoCollection<User> UsersCollection { get; }
    }
}