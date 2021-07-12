using MongoDB.Driver;
using Store.Core.Models;

namespace Store.Core.Interfaces
{
    public interface IDbClient
    {
        IMongoCollection<Record> GetRecordsCollection();
    }
}