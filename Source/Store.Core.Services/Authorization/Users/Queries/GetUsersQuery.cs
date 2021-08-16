using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Authorization.Users.Queries.Helpers;

namespace Store.Core.Services.Authorization.Users.Queries
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Role { get; set; }
        public Guid? CreatedBy { get; set; }
        public UsersSortBy SortBy { get; set; }
        public SortOrder Order { get; set; }
    }
    
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsersAsync(cancellationToken);

            if (users is null)
            {
                throw new ArgumentException("There are no users!");
            }

            var usersQuery = users.AsQueryable();

            usersQuery = usersQuery
                .FilterByName(request.Name)
                .FilterByRole(request.Role)
                .FilterByCreatedBy(request.CreatedBy)
                .FilterByStatus(request.IsActive);
            
            usersQuery = usersQuery.SortBy(request.SortBy, request.Order);

            return new GetUsersResponse()
            {
                Users = usersQuery.ToList(),
                UserCount = usersQuery.Count()
            };
        }
    }
}