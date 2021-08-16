using System.Collections.Generic;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Responses
{
    public class GetRolesResponse
    {
        public List<Role> Roles { get; set; }
        public int RoleCount { get; set; }
    }
}