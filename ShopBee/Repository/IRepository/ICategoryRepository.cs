using ShopBee.Data;
using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {

        void Update(Category category);
    }
}
