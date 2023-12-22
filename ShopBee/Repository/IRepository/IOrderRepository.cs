using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public int GetNumberOfOrders();

        public int CreateOrder(Order order);
    }
}
