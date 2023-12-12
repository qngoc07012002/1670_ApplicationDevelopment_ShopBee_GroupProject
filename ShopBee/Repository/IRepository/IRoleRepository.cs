using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IRoleRepository: IRepository<Role>
    {
        void Update(Role role);
    }
}
