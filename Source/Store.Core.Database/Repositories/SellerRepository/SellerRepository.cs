using System;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.SellerRepository
{
    public class SellerRepository : CrudRepositoryBase<Seller>, ISellerRepository
    {
        public SellerRepository(IDbContext context) : base(context.SellersCollection)
        {
        }

        protected override FilterDefinition<Seller> ConvertToFilterDefinition(IDbQuery query)
        {
            var builder = Builders<Seller>.Filter;

            var filter = builder.Empty;
            
            if (query.Id != Guid.Empty)
                filter = builder.And(filter, builder.Eq(x => x.Id, query.Id));

            return filter;
        }
    }
}