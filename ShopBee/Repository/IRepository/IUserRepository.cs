using ShopBee.Models;
using System.Linq.Expressions;
namespace ShopBee.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(string email, string password);
        void Register(User user);

        bool CheckRole(int userId, int roleId);
    }
}