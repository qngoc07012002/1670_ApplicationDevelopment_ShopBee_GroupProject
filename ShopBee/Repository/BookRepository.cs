using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private DatabaseContext _db;
        public BookRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
