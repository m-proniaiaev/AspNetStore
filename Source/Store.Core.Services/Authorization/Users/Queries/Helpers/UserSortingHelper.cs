using System;
using System.Linq;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;

namespace Store.Core.Services.Authorization.Users.Queries.Helpers
{
    public static class UserSortingHelper
    {
        public static IQueryable<User> SortBy(this IQueryable<User> source, UsersSortBy sortBy, SortOrder sortOrder)
        {
            if (source == null) return source;
        
            return sortBy switch
            {
                UsersSortBy.Name => source.SortBy(r => r.Name, sortOrder),
                UsersSortBy.Active => source.SortBy(u => u.IsActive, sortOrder),
                UsersSortBy.Created => source.SortBy(u=>u.Created, sortOrder),
                _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, "No such filter!")
            };
        }
    }
}