using System;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;

namespace Store.Core.Contracts.Domain
{
    public class BlackListRecord : IIdentity
    {
        public Guid Id { get; set; }
    }
}