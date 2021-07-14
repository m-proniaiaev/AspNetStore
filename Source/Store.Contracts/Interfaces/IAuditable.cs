using System;

namespace Store.Core.Interfaces
{
    public interface IAuditable
    {
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}