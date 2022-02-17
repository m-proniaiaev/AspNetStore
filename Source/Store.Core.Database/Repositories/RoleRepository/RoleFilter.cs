using System;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.RoleRepository
{
    public class RoleFilter : IDbQuery
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
        public bool? IsActive { get; set; }
    }
}