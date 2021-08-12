using System;
using System.Linq;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Authorization.Roles.Queries.GetRoles.Helpers
{
    public static class RolesFilteringHelper
    {
        public static IQueryable<Role> FilterBySoldStatus(this IQueryable<Role> source, string name)
        {
            if (source == null) return null;
            
           return !string.IsNullOrWhiteSpace(name)
               ? source.Where(role => string.Equals(role.Name, name, StringComparison.InvariantCultureIgnoreCase)) 
               : source;
        }
        
        public static IQueryable<Role> FilterByType(this IQueryable<Role> source, RoleType? type)
        {
            if (source == null) return null;

            return type.HasValue 
                ? source.Where(x => x.RoleType == type.Value)
                : source;
        }
        
        public static IQueryable<Role> FilterByActive(this IQueryable<Role> source, bool? active)
        {
            if (source == null) return null;

            return active.HasValue 
                ? source.Where(x => x.IsActive == active.Value) 
                : source;
        }
        
        public static IQueryable<Role> FilterByCreatedBy(this IQueryable<Role> source, Guid? created)
        {
            if (source == null) return null;

            return created.HasValue 
                ? source.Where(x => x.CreatedBy == created.Value) 
                : source;
        }
    }
}