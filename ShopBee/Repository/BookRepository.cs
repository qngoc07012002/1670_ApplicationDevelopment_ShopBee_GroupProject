using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly DatabaseContext _dbContext;
        public BookRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Book book)
        {
            _dbContext.Books.Update(book);
        }
    }
}
