using System.Collections.Generic;
using Store.Contracts.Models;

namespace Store.Contracts.Responses
{
    public class GetRecordsResponse
    {
        public List<Record> Records { get; set; }
        public int RecordCount { get; set; }
    }
}