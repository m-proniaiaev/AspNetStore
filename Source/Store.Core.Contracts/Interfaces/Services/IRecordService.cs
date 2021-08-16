using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecordsAsync(CancellationToken cts);
        Task AddRecordAsync(Record record, CancellationToken cts);
        Task<Record> GetRecordAsync(Guid id, CancellationToken cts);
        Task DeleteRecordAsync(Guid id, CancellationToken cts);
        Task UpdateRecordAsync(Record record, CancellationToken cts);
        Task MarkRecordAsSoldAsync(Guid id, Guid editor, CancellationToken cts);
    }
}