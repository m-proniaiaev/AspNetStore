using System;
using System.Linq;
using MongoDB.Driver.Linq;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.GetRecords.Helpers
{
    public static class FilteringHelper
    {
        public static IQueryable<Record> FilterBySoldStatus(this IQueryable<Record> source, bool? isSold)
        {
            if (source == null) return null;
            
            if(isSold.HasValue)
                return isSold.Value
                ? source.Where(x => x.IsSold)
                : source.Where(x => x.IsSold != true);
            
            return source;
        }

        public static IQueryable<Record> FilterByName(this IQueryable<Record> source, string name)
        {
            if (source == null) return null;

            return !string.IsNullOrWhiteSpace(name) 
                ? source.Where(record => string.Equals(record.Name, name, StringComparison.InvariantCultureIgnoreCase)) 
                : source;
        }

        public static IQueryable<Record> FilterBySeller(this IQueryable<Record> source, string seller)
        {
            if (source == null) return null;
            
            return !string.IsNullOrWhiteSpace(seller) 
                ? source.Where(record => string.Equals(record.Seller, seller, StringComparison.InvariantCultureIgnoreCase))
                : source;
        }

        public static IQueryable<Record> FilterByPrice(this IQueryable<Record> source, decimal? price)
        {
            if (source == null) return null;

            return price.HasValue
                ? source.Where(record => record.Price == price.Value)
                : source;
        }

        public static IQueryable<Record> FilterByCreated(this IQueryable<Record> source, DateTime? from, DateTime? to)
        {
            if (source == null) return null;

            if (from.HasValue)
                source = source.Where(r => r.Created >= from.Value);
            if (to.HasValue)
                source = source.Where(r => r.Created <= to.Value);

            return source;
        }
        
        public static IQueryable<Record> FilterBySold(this IQueryable<Record> source, DateTime? from, DateTime? to)
        {
            if (source == null) return null;

            if (from.HasValue)
                source = source.Where(r => r.SoldDate >= from.Value);
            if (to.HasValue)
                source = source.Where(r => r.SoldDate <= to.Value);

            return source;
        }
    }
}