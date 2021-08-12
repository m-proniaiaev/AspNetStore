using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Internal.Records.Queries.CreateRecord;
using Store.Core.Internal.Records.Queries.UpdateRecord;

namespace Store.Core.Common.Interfaces
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecordsAsync(CancellationToken cts);
        Task AddRecordAsync(CreateRecordCommand request, Guid id, CancellationToken cts);
        Task<Record> GetRecordAsync(Guid id, CancellationToken cts);
        Task DeleteRecordAsync(Guid id, CancellationToken cts);
        Task UpdateRecordAsync(UpdateRecordCommand request, Record origin, CancellationToken cts);
        Task MarkRecordAsSoldAsync(Guid id, CancellationToken cts);
    }
}