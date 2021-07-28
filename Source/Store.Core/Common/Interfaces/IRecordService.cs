using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Services.Records.Queries.CreateRecord;

namespace Store.Core.Common.Interfaces
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecords();
        Task AddRecordAsync(CreateRecordCommand request, Guid id);
        Task<Record> GetRecord(Guid id);
        Task DeleteRecord(Guid id);
        Task<Record> UpdateRecord(Record record);
        Task MarkRecordAsSold(Guid id);

    }
}