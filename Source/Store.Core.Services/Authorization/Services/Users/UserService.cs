using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;

namespace Store.Core.Authorization.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDbClient client)
        {
            _users = client.GetUsersCollection();
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cts)
        {
            return await _users.Find(x => true).ToListAsync(cts);
        }

        public async Task AddUserAsync(User user, CancellationToken cts)
        {
            await _users.InsertOneAsync(user, cancellationToken: cts);
        }

        public async Task<User> GetUserAsync(Guid id, CancellationToken cts)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync(cts);
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cts)
        {
            await _users.DeleteOneAsync(user => user.Id == id, cts);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cts)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user, cancellationToken: cts);
        }

        public async Task ChangeUserPassword(User user, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.Hash, user.Hash)
                .Set(x => x.Salt, user.Salt);

            await _users.UpdateOneAsync(u => u.Id == user.Id, update, cancellationToken: cts);
        }

        public async Task ChangeUserRole(User user, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.Role, user.Role);

            await _users.UpdateOneAsync(u => u.Id == user.Id, update, cancellationToken: cts);
        }

        public async Task MarkUserAsDisabledAsync(Guid id, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.IsActive, false);

            await _users.UpdateOneAsync(user => user.Id == id, update, cancellationToken: cts);
        }
    }
}