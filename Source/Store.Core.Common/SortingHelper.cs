using System;
using System.Linq;
using System.Linq.Expressions;
using Store.Core.Contracts.Enums;

namespace Store.Core.Common
{
    public static class SortingHelper
    {
        public static IQueryable<TSource> SortBy<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> sortBy, SortOrder order)
        {
            return order == SortOrder.Desc 
                ? source.OrderByDescending(sortBy)
                : source.OrderBy(sortBy);
        }
    }
}