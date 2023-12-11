using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly DatabaseContext _dbContext;
        public CartRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Cart cart)
        {
            _dbContext.Carts.Update(cart);
        }
    }
}
