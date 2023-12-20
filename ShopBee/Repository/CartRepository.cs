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

    }
}
