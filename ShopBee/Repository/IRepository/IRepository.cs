using ShopBee.Models;
using System.Linq.Expressions;

namespace ShopBee.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
        int GetNumberOfBooks();
        int GetNumberOfUsers();
        int GetNumberOfOrders();
        int GetNumberOfStores();
    }
}