using System;
using Store.Core.Database.Database;

namespace Store.Core.Database.Repositories.SellerRepository
{
    public class SellerFilter : IDbQuery
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
    }
}