using System;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Responses;

namespace Store.Core.Services.Authorization.Users.Queries
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Role { get; set; }
        public Guid? CreatedBy { get; set; }
        public SortOrder Order { get; set; }
    }
}