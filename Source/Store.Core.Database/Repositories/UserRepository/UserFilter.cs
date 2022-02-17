using System;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.UserRepository
{
    public class UserFilter : IDbQuery
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
        public bool? IsUserActive { get; set; }
    }
}