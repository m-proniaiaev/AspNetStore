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
        Task<List<Record>> GetRecords();
        Task AddRecordAsync(CreateRecordCommand request, Guid id);
        Task<Record> GetRecord(Guid id);
        Task DeleteRecord(Guid id);
        Task<Record> UpdateRecord(UpdateRecordCommand request, Record origin, CancellationToken cts);
        Task MarkRecordAsSold(Guid id);

    }
}