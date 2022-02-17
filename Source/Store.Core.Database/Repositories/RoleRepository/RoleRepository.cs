using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.RoleRepository
{
    public class RoleRepository : CrudRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext context) : base(context.RolesCollection)
        {
        }

        protected override FilterDefinition<Role> ConvertToFilterDefinition(IDbQuery query)
        {
            if (query is not RoleFilter roleQuery)
                return FilterDefinition<Role>.Empty;

            var builder = Builders<Role>.Filter;

            var filter = builder.Empty;

            if (roleQuery.Id != Guid.Empty)
                filter = builder.And(filter, builder.Eq(x => x.Id, query.Id));

            if (roleQuery.IsRoleActive.HasValue)
                filter = builder.And(filter, builder.Eq(r => r.IsActive, roleQuery.IsRoleActive.Value));

            return filter;

        }

        public async Task DisableAsync(Guid id, Guid editor, CancellationToken cts)
        {
            var update = Builders<Role>.Update
                .Set(x => x.IsActive, false)
                .Set(x => x.Edited, DateTime.Now)
                .Set(x => x.EditedBy, editor);
            
            await _collection.UpdateOneAsync(CreateIdFilter(id), update, cancellationToken: cts);
        }
    }
}