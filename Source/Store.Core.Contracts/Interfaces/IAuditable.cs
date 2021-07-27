using System;

namespace Store.Core.Contracts.Interfaces
{
    public interface IAuditable
    {
        public string Seller { get; set; }
        public DateTime Created { get; set; }
    }
}