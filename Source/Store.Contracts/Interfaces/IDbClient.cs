using MongoDB.Driver;
using Store.Contracts.Models;

namespace Store.Core.Interfaces
{
    public interface IDbClient
    {
        IMongoCollection<Record> GetRecordsCollection();
    }
}