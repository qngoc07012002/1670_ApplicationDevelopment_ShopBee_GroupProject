using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {

        void Update(Cart cart);
    }
}
