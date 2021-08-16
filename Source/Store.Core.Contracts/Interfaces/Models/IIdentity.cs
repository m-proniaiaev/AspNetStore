using System;

namespace Store.Core.Contracts.Interfaces.Models
{
    public interface IIdentity
    {
        public Guid Id { get; set; }
    }
}