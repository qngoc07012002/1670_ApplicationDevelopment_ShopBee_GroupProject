using Microsoft.EntityFrameworkCore;
using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private DatabaseContext _db;
        public CartRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }
        public int GetNumbersOfItems(int userId)
        {
            int count = _db.Carts.Count(c=> c.UserID == userId);
            return count;
        }
        public List<Cart> GetCartByUser(int userId)
        {
            var query = _db.Carts.Where(c => c.UserID == userId);
            string includeProperties = "Book";
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return query.ToList();
        }

     
    }
}
