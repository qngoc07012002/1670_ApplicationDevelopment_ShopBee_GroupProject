using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        int GetNumberOfBooks();

        List<Book> GetAllBookByCategory(int? categoryId);

        public List<Book> GetBookBySearch(string searchString);

        public List<Book> GetAllBookSort();
    }
}
