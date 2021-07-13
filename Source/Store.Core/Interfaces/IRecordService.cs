using System;
using System.Collections.Generic;
using Store.Core.Models;

namespace Store.Core.Interfaces
{
    public interface IRecordService
    {
        IEnumerable<Record> GetRecords();
        Record AddRecord(Record record);
        Record GetRecord(Guid id);
        void DeleteRecord(Guid id);
        Record UpdateRecord(Record record);
        void MarkRecordAsSold(Guid id);

    }
}