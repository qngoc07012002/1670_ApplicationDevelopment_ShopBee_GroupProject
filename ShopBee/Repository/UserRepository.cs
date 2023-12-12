using Microsoft.EntityFrameworkCore;
using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private DatabaseContext _db;
        public UserRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public User Login(string email, string password)
        {
            User? account = _db.Users.FirstOrDefault(m => m.Email == email && m.Password == password);
            return account;

        }

        public void Register(User user)
        {
            throw new NotImplementedException();
        }

        public bool CheckRole(int userId, int roleId)
        {
            var count = _db.UserRoles.Count(m => m.UserId == userId && m.RoleId == roleId);
            if (count == 0)
            {
                return false;
            }
            else return true;
        }
    }
}