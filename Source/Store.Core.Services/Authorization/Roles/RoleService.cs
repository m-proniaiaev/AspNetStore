using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Repositories.RoleRepository;

namespace Store.Core.Services.Authorization.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        
        public RoleService(IRoleRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<Role>> GetRolesAsync(CancellationToken cts)
        {
            var filter = new RoleFilter();

            if (_currentUserService.RoleType != RoleType.Administrator)
                filter.IsRoleActive = true;
            
            return await _repository.FindManyAsync(filter, cts);
        }

        public async Task<Role> GetRoleAsync(Guid id, CancellationToken cts)
        {
            var filter = new RoleFilter()
            {
                Id = id,
                Limit = 1,
            };

            if (_currentUserService.RoleType != RoleType.Administrator)
                filter.IsRoleActive = true;
            
            return (await _repository.FindManyAsync(filter, cts)).FirstOrDefault();
        }
        
        public async Task UpdateRoleAsync(Role model, CancellationToken cts)
        {
            await _repository.UpdateAsync(model, cts);
        }
        
        public async Task CreateRoleAsync(Role role, CancellationToken cts)
        {
            await _repository.CreateAsync(role, cts);
        }

        public async Task DisableRoleAsync(Guid id, Guid editor, CancellationToken cts)
        {
            await _repository.DisableAsync(id, editor, cts);
        }

        public async Task DeleteRoleAsync(Guid id, CancellationToken cts)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.Id, id);
            await _repository.DeleteAsync(filter, cts);
        }
    }
}