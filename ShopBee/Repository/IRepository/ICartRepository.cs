using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        public int GetNumbersOfItems(int userId);
        public List<Cart> GetCartByUser(int userId);

        
    }
}
