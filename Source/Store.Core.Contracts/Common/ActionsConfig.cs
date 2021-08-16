using System.Collections.Generic;
using Store.Core.Contracts.Enums;

namespace Store.Core.Contracts.Common
{
    public class ActionsConfig
    {
        public Dictionary<RoleType, string[]> Actions { get; set; }
    }
}