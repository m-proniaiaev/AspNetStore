using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.AuthHost.Common.Interfaces;
using Store.Core.Services.AuthHost.Services.Roles.Queries.CreateRole;

namespace Store.Core.Services.AuthHost.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IMongoCollection<Role> _roles;

        public RoleService(IDbClient client)
        {
            _roles = client.GetRolesCollection();
        }

        public async Task<List<Role>> GetRolesAsync(CancellationToken cts)
        {
            return await _roles.Find(role => true).ToListAsync(cts);
        }

        public async Task<Role> GetRoleAsync(Guid id, CancellationToken cts)
        {
            return await _roles.Find(role => role.Id == id).FirstOrDefaultAsync(cts);
        }
        
        public async Task UpdateRoleAsync(Role model, CancellationToken cts)
        {
            await _roles.ReplaceOneAsync(r => r.Id == model.Id, model, cancellationToken: cts);
        }
        
        public async Task CreateRoleAsync(CreateRoleCommand request, Guid id, string[] actions, CancellationToken cts)
        {
            var role = new Role()
            {
                Id = id,
                Name = request.Name,
                IsActive = request.IsActive,
                RoleType = request.RoleType,
                Actions = actions,
                Created = DateTime.Now,
                CreatedBy = Guid.Empty //TODO
            };
            
            await _roles.InsertOneAsync(role, cancellationToken: cts);
        }

        public async Task DisableRoleAsync(Guid id, CancellationToken cts)
        {
            var update = Builders<Role>.Update
                .Set("IsActive", false);

            await _roles.UpdateOneAsync(role => role.Id == id, update, cancellationToken: cts);
        }

        public async Task DeleteRoleAsync(Guid id, CancellationToken cts)
        {
            await _roles.DeleteOneAsync(role => role.Id == id, cts);
        }
    }
}