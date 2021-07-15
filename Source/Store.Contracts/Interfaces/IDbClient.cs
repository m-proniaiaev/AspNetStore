using MongoDB.Driver;
using Store.Contracts.Models;

namespace Store.Contracts.Interfaces
{
    public interface IDbClient
    {
        IMongoCollection<Record> GetRecordsCollection();
    }
}