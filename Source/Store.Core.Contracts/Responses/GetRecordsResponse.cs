using System.Collections.Generic;
using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Responses
{
    public class GetRecordsResponse
    {
        public List<Record> Records { get; set; }
        public int RecordCount { get; set; }
    }
}