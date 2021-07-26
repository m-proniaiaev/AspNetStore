using MongoDB.Driver;
using Store.Core.Contracts.Models;

namespace Store.Core.Database.Database
{
    public interface IDbClient
    {
        IMongoCollection<Record> GetRecordsCollection();
    }
}