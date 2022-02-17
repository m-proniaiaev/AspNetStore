using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.RecordRepository
{
    public interface IRecordRepository : ICrudRepository<Record>
    {
        Task SellAsync(Guid id, Guid editor, CancellationToken cts);
    }
}