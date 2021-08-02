using System;
using System.Linq;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Sellers.Queries.GetSellers.Helpers
{
    public static class SellersFilteringHelper
    {
        public static IQueryable<Seller> FilterByName(this IQueryable<Seller> source, string name)
        {
            if (source == null) return null;

            return !string.IsNullOrWhiteSpace(name) 
                ? source.Where(r => string.Equals(r.Name, name, StringComparison.InvariantCultureIgnoreCase))
                : source;
        }

        public static IQueryable<Seller> FilterByTypes(this IQueryable<Seller> source, RecordType[] recordTypes)
        {
            if (source == null) return null;

            return recordTypes.Length > 0
                ? source.Where(r => r.RecordType.Intersect(recordTypes).Count() == recordTypes.Length)
                : source;
        }
        
        public static IQueryable<Seller> FilterByCreatedBy(this IQueryable<Seller> source, Guid? createdBy)
        {
            if (source == null) return null;

            return createdBy.HasValue
                ? source.Where(r => r.CreatedBy == createdBy.Value)
                : source;
        }
        public static IQueryable<Seller> FilterByCreated(this IQueryable<Seller> source, DateTime? from, DateTime? to)
        {
            if (source == null) return null;

            if (from.HasValue)
                source = source.Where(r => r.Created >= from.Value);
            if (to.HasValue)
                source = source.Where(r => r.Created <= to.Value);

            return source;
        }
    }
}