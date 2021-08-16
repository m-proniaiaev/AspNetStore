using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.Authorization.Roles.Queries.CreateRole;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization.Roles
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
        
        public async Task CreateRoleAsync(Role role, CancellationToken cts)
        {
            await _roles.InsertOneAsync(role, cancellationToken: cts);
        }

        public async Task DisableRoleAsync(Guid id, Guid editor, CancellationToken cts)
        {
            var update = Builders<Role>.Update
                .Set(x => x.IsActive, false)
                .Set(x => x.Edited, DateTime.Now)
                .Set(x => x.EditedBy, editor);

            await _roles.UpdateOneAsync(role => role.Id == id, update, cancellationToken: cts);
        }

        public async Task DeleteRoleAsync(Guid id, CancellationToken cts)
        {
            await _roles.DeleteOneAsync(role => role.Id == id, cts);
        }
    }
}