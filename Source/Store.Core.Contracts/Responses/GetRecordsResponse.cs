using System.Collections.Generic;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Responses
{
    public class GetRecordsResponse
    {
        public List<Record> Records { get; set; }
        public int RecordCount { get; set; }
    }
}