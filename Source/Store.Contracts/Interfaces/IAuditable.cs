using System;

namespace Store.Contracts.Interfaces
{
    public interface IAuditable
    {
        public string Seller { get; set; }
        public DateTime Created { get; set; }
    }
}