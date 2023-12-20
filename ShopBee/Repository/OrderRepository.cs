using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private DatabaseContext _db;
        public OrderRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
