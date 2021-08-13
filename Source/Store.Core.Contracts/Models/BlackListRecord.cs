using System;
using Store.Core.Contracts.Interfaces;

namespace Store.Core.Contracts.Models
{
    public class BlackListRecord : IIdentity
    {
        public Guid Id { get; set; }
        public bool Permanent { get; set; }
    }
}