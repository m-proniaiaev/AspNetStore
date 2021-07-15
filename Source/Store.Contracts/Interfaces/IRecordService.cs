using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Contracts.Models;

namespace Store.Core.Interfaces
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