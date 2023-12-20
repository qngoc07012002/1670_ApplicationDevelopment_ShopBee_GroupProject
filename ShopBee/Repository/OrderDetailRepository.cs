using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private DatabaseContext _db;
        public OrderDetailRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
