using System.Collections.Generic;
using Store.Core.Contracts.Enums;

namespace Store.Core.Contracts.Models
{
    public class ActionsConfig
    {
        public Dictionary<RoleType, string[]> Actions { get; set; }
    }
}