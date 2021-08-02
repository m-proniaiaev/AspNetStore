using System;

namespace Store.Core.Contracts.Interfaces
{
    public interface IAuditable
    {
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
    }
}