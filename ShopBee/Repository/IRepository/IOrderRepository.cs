using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetNumberOfOrders();
    }
}
