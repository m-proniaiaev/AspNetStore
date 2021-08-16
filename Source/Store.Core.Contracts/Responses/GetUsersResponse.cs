using System.Collections.Generic;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Responses
{
    public class GetUsersResponse
    {
        public List<User> Users { get; set; }
        public int UserCount { get; set; }
    }
}