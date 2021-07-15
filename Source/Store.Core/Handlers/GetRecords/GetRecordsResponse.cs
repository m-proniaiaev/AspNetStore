using System.Collections.Generic;
using Store.Contracts.Models;

namespace Store.Core.Handlers.GetRecords
{
    public class GetRecordsResponse
    {
        public List<Record> Records { get; set; }
        public int RecordCount { get; set; }
    }
}