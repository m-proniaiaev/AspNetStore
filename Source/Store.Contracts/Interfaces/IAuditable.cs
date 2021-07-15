using System;

namespace Store.Contracts.Interfaces
{
    public interface IAuditable
    {
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}