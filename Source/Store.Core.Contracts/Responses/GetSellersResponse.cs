using System.Collections.Generic;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Responses
{
    public class GetSellersResponse
    {
        public List<Seller> Sellers { get; set; }
        public int SellerCount { get; set; }
    }
}