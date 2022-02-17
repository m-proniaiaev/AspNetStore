using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Repositories.UserRepository;

namespace Store.Core.Services.Authorization.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IUserRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cts)
        {
            var filter = new UserFilter();
            
            if (_currentUserService.RoleType != RoleType.Administrator)
                filter.IsUserActive = true;
            
            return await _repository.FindManyAsync(filter, cts);
        }

        public async Task AddUserAsync(User user, CancellationToken cts)
        {
            await _repository.CreateAsync(user, cts);
        }

        public async Task<User> GetUserAsync(Guid id, CancellationToken cts)
        {
            var filter = new UserFilter()
            {
                Id = id,
                Limit = 1,
            };

            if (_currentUserService.RoleType != RoleType.Administrator)
                filter.IsUserActive = true;
            
            return (await _repository.FindManyAsync(filter, cts)).FirstOrDefault();
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cts)
        {
            var filter = Builders<User>.Filter.Eq(r => r.Id, id);
            await _repository.DeleteAsync(filter, cts);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cts)
        {
            await _repository.UpdateAsync(user, cts);
        }

        public async Task ChangeUserPassword(User user, CancellationToken cts)
        {
            await _repository.ChangePassword(user, cts);
        }

        public async Task ChangeUserRole(User user, CancellationToken cts)
        {
            await _repository.ChangeRole(user, cts);
        }

        public async Task MarkUserAsDisabledAsync(Guid id, Guid editor, CancellationToken cts)
        {
            await _repository.MarkAsDisabledAsync(id, editor, cts);
        }
    }
}