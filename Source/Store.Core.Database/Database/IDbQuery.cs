using System;

namespace Store.Core.Database.Database
{
    public interface IDbQuery
    {
        Guid Id { get; set; }
        int Limit { get; set; }
        int Skip { get; set; }
    }
}