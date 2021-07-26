using System;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Records.Queries.GetRecords.Helpers;

namespace Store.Core.Services.Records.Queries.GetRecords
{
    public class GetRecordsQuery : IRequest<GetRecordsResponse>
    {
        public bool? IsSold { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Seller { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? SoldFrom { get; set; }
        public DateTime? SoldTo { get; set; }
        public RecordSortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}