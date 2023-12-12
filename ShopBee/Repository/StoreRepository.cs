using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly DatabaseContext _dbContext;
        public StoreRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Store store)
        {
            _dbContext.Stores.Update(store);
        }
    }
}
