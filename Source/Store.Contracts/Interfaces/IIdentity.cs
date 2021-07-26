using System;

namespace Store.Contracts.Interfaces
{
    public interface IIdentity
    {
        public Guid Id { get; set; }
    }
}