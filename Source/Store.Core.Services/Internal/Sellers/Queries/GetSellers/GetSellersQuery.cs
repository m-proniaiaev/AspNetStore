using System;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Responses;
using Store.Core.Internal.Sellers.Queries.GetSellers.Helpers;

namespace Store.Core.Internal.Sellers.Queries.GetSellers
{
    public class GetSellersQuery : IRequest<GetSellersResponse>
    {
        public string Name { get; set; }
        public RecordType[] RecordType { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public SellersSortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}