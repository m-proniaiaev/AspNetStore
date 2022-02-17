using Store.Core.Contracts.Domain;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.SellerRepository
{
    public interface ISellerRepository : ICrudRepository<Seller>
    {
    }
}