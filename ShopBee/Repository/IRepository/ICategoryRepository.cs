using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetAllCategory();

    }
}
