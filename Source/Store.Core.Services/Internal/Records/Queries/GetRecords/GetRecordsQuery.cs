using System;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Internal.Records.Queries.GetRecords.Helpers;

namespace Store.Core.Services.Internal.Records.Queries.GetRecords
{
    public class GetRecordsQuery : IRequest<GetRecordsResponse>
    {
        public string Name { get; set; }
        public string Seller { get; set; }
        public decimal? Price { get; set; }
        public bool? IsSold { get; set; }
        public RecordType? RecordType { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? SoldFrom { get; set; }
        public DateTime? SoldTo { get; set; }
        public RecordSortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}