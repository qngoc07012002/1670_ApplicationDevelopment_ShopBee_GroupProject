using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {

        void Update(Book book);
    }
}
