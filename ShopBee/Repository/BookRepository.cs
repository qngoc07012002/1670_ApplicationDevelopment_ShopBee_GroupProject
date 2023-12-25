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

        public List<Book> GetAllBookByCategory(int? categoryId)
        {
            var query = _db.Books.Where(c => c.CategoryId == categoryId || c.IsDeleted != 1);
            return query.ToList();
        }

        public List<Book> GetBookBySearch(string searchString)
        {
            var query = _db.Books.Where(c => c.Name.Contains(searchString) || c.Author.Contains(searchString) || c.IsDeleted != 1);
            return query.ToList();
        }

        public List<Book> GetAllBookSort()
        {

            var query = _db.Books.Where(c => c.IsDeleted != 1).OrderByDescending(c => c.DiscountPrice);
            return query.ToList();
        }

        public int GetNumberOfBooks()
        {
            return _db.Books.Count(c=> c.IsDeleted != 1);
        }
    }
}
