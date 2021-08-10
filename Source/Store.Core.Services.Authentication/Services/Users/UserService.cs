using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.AuthHost.Common.Interfaces;

namespace Store.Core.Services.AuthHost.Services.Users
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

        public async Task AddUserAsync(Guid id, CancellationToken cts)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(Guid id, CancellationToken cts)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync(cts);
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cts)
        {
            await _users.DeleteOneAsync(user => user.Id == id, cts);
        }

        public async Task UpdateUserAsync(CancellationToken cts)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeUserPassword(CancellationToken cts)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeUserRole(CancellationToken cts)
        {
            throw new NotImplementedException();
        }

        public async Task MarkUserAsDisabledAsync(Guid id, CancellationToken cts)
        {
            var update = Builders<User>.Update
                .Set(x => x.IsActive, false);

            await _users.UpdateOneAsync(user => user.Id == id, update, cancellationToken: cts);
        }
    }
}