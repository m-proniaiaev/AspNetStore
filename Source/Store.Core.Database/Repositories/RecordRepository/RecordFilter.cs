using System;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.RecordRepository
{
    public class RecordFilter : IDbQuery
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
        public bool? IsSold { get; set; }
    }
}