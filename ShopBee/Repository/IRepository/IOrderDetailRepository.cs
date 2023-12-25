using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public List<OrderDetail> GetOrderDetailsByOrder(int orderId);


    }
}
