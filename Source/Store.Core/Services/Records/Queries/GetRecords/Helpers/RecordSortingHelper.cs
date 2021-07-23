using System;
using System.Linq;
using Store.Contracts.Enums;
using Store.Contracts.Models;
using Store.Core.Common;

namespace Store.Core.Services.Records.Queries.GetRecords.Helpers
{
    public static class RecordSortingHelper
    {
        public static IQueryable<Record> SortBy(this IQueryable<Record> source, RecordSortBy sortBy, SortOrder sortOrder)
        {
            if (source == null) return source;
        
            return sortBy switch
            {
                RecordSortBy.Name => source.SortBy(r => r.Name, sortOrder),
                RecordSortBy.Seller => source.SortBy(r=>r.Seller, sortOrder),
                _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, "No such filter!")
            };
        }
    }
}