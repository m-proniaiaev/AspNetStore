using System;

namespace Store.Core.Contracts.Interfaces
{
    public interface IIdentity
    {
        public Guid Id { get; set; }
    }
}