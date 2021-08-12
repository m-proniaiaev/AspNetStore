using System;
using System.Linq;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Authorization.Users.Queries.Helpers
{
    public static class UsersFilteringHelper
    {
        public static IQueryable<User> FilterByCreatedBy(this IQueryable<User> source, Guid? created)
        {
            if (source == null) return null;

            return created.HasValue 
                ? source.Where(x => x.CreatedBy == created.Value) 
                : source;
        }
        
        public static IQueryable<User> FilterByRole(this IQueryable<User> source, Guid? role)
        {
            if (source == null) return null;

            return role.HasValue 
                ? source.Where(x => x.Role == role.Value) 
                : source;
        }
        
        public static IQueryable<User> FilterByStatus(this IQueryable<User> source, bool? isActive)
        {
            if (source == null) return null;

            return isActive.HasValue 
                ? source.Where(x => x.IsActive == isActive.Value) 
                : source;
        }
        
        public static IQueryable<User> FilterByName(this IQueryable<User> source, string isActive)
        {
            if (source == null) return null;

            return string.IsNullOrWhiteSpace(isActive)
                ? source.Where(x => x.Name == isActive) 
                : source;
        }
    }
}