using System;
using System.Linq;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Common;

namespace Store.Core.Services.Internal.Sellers.Queries.GetSellers.Helpers
{
    public static class SellersSortHelper
    {
        public static IQueryable<Seller> SortBy(this IQueryable<Seller> source, SellersSortBy sortBy, SortOrder sortOrder)
        {
            if (source == null) return source;
        
            return sortBy switch
            {
                SellersSortBy.Name => source.SortBy(r => r.Name, sortOrder),
                SellersSortBy.RecordType => source.SortBy(r => r.RecordType, sortOrder),
                _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, "No such filter!")
            };
        }
    }
}