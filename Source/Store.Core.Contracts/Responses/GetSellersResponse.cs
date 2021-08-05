using System.Collections.Generic;
using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Responses
{
    public class GetSellersResponse
    {
        public List<Seller> Sellers { get; set; }
        public int SellerCount { get; set; }
    }
}