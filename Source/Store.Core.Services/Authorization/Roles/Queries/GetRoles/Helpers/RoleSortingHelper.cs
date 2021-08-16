using System;
using System.Linq;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Services.Common;

namespace Store.Core.Services.Authorization.Roles.Queries.GetRoles.Helpers
{
    public static class RoleSortingHelper
    {
        public static IQueryable<Role> SortBy(this IQueryable<Role> source, RoleSortBy sortBy, SortOrder sortOrder)
        {
            if (source == null) return source;
        
            return sortBy switch
            {
                RoleSortBy.Name => source.SortBy(r => r.Name, sortOrder),
                RoleSortBy.Type => source.SortBy(r => r.RoleType, sortOrder),
                _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, "No such filter!")
            };
        }
    }
}