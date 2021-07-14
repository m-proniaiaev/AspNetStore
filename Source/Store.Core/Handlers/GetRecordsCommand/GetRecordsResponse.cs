using Store.Contracts.Models;

namespace Store.Core.Handlers.GetRecordsCommand
{
    public class GetRecordsResponse
    {
        public Record[] Records { get; set; }
    }
}