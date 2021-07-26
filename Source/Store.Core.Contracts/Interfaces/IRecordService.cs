using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Interfaces
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecords();
        Task<Record> AddRecord(Record record);
        Task<Record> GetRecord(Guid id);
        Task DeleteRecord(Guid id);
        Task<Record> UpdateRecord(Record record);
        Task MarkRecordAsSold(Guid id);

    }
}