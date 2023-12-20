using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        int GetNumberOfBooks();
    }
}
