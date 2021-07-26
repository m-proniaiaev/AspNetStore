using MongoDB.Driver;
using Store.Contracts.Models;

namespace Store.Database.Database
{
    public interface IDbClient
    {
        IMongoCollection<Record> GetRecordsCollection();
    }
}