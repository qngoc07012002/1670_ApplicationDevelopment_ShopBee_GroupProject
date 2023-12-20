using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private DatabaseContext _db;
        public CategoryRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
