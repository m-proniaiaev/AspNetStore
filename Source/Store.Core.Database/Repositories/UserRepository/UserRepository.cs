using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.UserRepository
{
    public class UserRepository : CrudRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context.UsersCollection)
        {
        }

        protected override FilterDefinition<User> ConvertToFilterDefinition(IDbQuery query)
        {
            if (query is not UserFilter userFilter)
                return FilterDefinition<User>.Empty;
            
            var builder = Builders<User>.Filter;
            
            var filter = builder.Empty;

            if (userFilter.Id != Guid.Empty)
                filter = builder.And(filter, builder.Eq(x => x.Id, query.Id));

            if (userFilter.IsUserActive.HasValue)
                filter = builder.And(filter, builder.Eq(r => r.IsActive, userFilter.IsUserActive.Value));

            return filter;
        }

        public async Task ChangePassword(User user, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.Hash, user.Hash)
                .Set(x => x.Salt, user.Salt);

            await _collection.UpdateOneAsync(ConvertToFilterDefinition(user), update, cancellationToken: cts);
        }

        public async Task ChangeRole(User user, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.Role, user.Role);

            await _collection.UpdateOneAsync(ConvertToFilterDefinition(user), update, cancellationToken: cts);
        }

        public async Task MarkAsDisabledAsync(Guid id, Guid editor, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.IsActive, false)
                .Set(x => x.Edited, DateTime.Now)
                .Set(x => x.EditedBy, editor);

            await _collection.UpdateOneAsync(CreateIdFilter(id), update, cancellationToken: cts);
        }
    }
}