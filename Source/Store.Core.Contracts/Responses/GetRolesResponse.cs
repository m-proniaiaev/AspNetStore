using System.Collections.Generic;
using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Responses
{
    public class GetRolesResponse
    {
        public List<Role> Roles { get; set; }
        public int RoleCount { get; set; }
    }
}