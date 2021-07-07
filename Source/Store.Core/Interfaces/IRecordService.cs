using System.Collections.Generic;
using Store.Core.Models;

namespace Store.Core.Interfaces
{
    public interface IRecordService
    {
        IEnumerable<Record> GetRecords();
    }
}