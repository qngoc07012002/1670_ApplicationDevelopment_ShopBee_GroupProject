using Microsoft.EntityFrameworkCore;
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
        public List<OrderDetail> GetOrderDetailsByOrder(int orderId)
        {
            var query = _db.OrderDetails.Where(c => c.OrderId == orderId);
            string includeProperties = "Book";
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return query.ToList();
        }
    }
}
