using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Services.Records.Queries.CreateRecord;
using Store.Core.Services.Records.Queries.UpdateRecord;

namespace Store.Core.Common.Interfaces
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecords(CancellationToken cts);
        Task AddRecordAsync(CreateRecordCommand request, Guid id, CancellationToken cts);
        Task<Record> GetRecordAsync(Guid id, CancellationToken cts);
        Task DeleteRecord(Guid id, CancellationToken cts);
        Task<Record> UpdateRecord(UpdateRecordCommand request, Record origin, CancellationToken cts);
        Task MarkRecordAsSold(Guid id, CancellationToken cts);

    }
}