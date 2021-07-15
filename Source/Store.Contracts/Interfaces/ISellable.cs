using System;

namespace Store.Contracts.Interfaces
{
    public interface ISellable
    {
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime? SoldDate { get; set; }
    }
}