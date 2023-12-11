using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DatabaseContext _dbContext;
        public OrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Order order)
        {
            _dbContext.Orders.Update(order);
        }
    }
}
