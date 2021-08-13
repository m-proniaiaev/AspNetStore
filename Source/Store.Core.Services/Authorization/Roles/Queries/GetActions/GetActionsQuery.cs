using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;

namespace Store.Core.Services.Authorization.Roles.Queries.GetActions
{
    public class GetActionsQuery : IRequest<GetActionsResponse>
    {
        public RoleType RoleType { get; set; }
    }
    
    public class GetActionsQueryHandler : IRequestHandler<GetActionsQuery, GetActionsResponse>
    {
        private readonly Dictionary<RoleType, string[]> _roles;

        public GetActionsQueryHandler(IOptions<ActionsConfig> options)
        {
            _roles = options.Value.Actions;
        }
        
        public Task<GetActionsResponse> Handle(GetActionsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetActionsResponse
            {
                RoleType = request.RoleType,
                Actions = _roles[request.RoleType]
            });
        }
    }
}