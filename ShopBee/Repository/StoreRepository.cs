using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private DatabaseContext _db;
        public StoreRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
