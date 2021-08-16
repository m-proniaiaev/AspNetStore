using System;
using Store.Core.Contracts.Interfaces.Models;

namespace Store.Core.Contracts.Common
{
    public class BlackListRecord : IIdentity
    {
        public Guid Id { get; set; }
    }
}