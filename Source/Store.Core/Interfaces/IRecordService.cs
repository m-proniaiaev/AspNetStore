using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core.Models;

namespace Store.Core.Interfaces
{
    public interface IRecordService
    {
        List<Record> GetRecords();
        Task<Record> AddRecord(Record record);
        Record GetRecord(Guid id);
        Task DeleteRecord(Guid id);
        Task<Record> UpdateRecord(Record record);
        Task MarkRecordAsSold(Guid id);

    }
}