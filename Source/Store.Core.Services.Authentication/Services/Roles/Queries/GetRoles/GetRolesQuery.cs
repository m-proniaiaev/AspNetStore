using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Responses;
using Store.Core.Services.AuthHost.Common.Interfaces;
using Store.Core.Services.AuthHost.Services.Roles.Queries.GetRoles.Helpers;

namespace Store.Core.Services.AuthHost.Services.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<GetRolesResponse>
    {
        public string Name { get; set; }
        public RoleType? RoleType { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public RoleSortBy SortBy { get; set; }
        public SortOrder Order { get; set; }
    }
    
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, GetRolesResponse>
    {
        private readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesResponse> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRolesAsync(cancellationToken);
            
            if (roles == null)
                throw new ArgumentException("No records in database!");

            var rolesQuery = roles.AsQueryable();

            rolesQuery = rolesQuery
                .FilterBySoldStatus(request.Name)
                .FilterByType(request.RoleType)
                .FilterByActive(request.IsActive)
                .FilterByCreatedBy(request.CreatedBy);

            rolesQuery = rolesQuery.SortBy(request.SortBy, request.Order);

            return new GetRolesResponse
            {
                Roles = rolesQuery.ToList(),
                RoleCount = rolesQuery.Count()
            };
        }
    }
}