using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);

    }
}
