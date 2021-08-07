using Store.Core.Contracts.Enums;

namespace Store.Core.Contracts.Responses
{
    public class GetActionsResponse
    {
        public RoleType RoleType { get; set; }
        public string[] Actions { get; set; }
    }
}