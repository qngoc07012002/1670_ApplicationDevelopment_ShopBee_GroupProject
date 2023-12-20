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

        public string GetUserRoles(int userId)
        {
            var userRoles = (from ur in _db.UserRoles
                             join r in _db.Roles on ur.RoleId equals r.Id
                             where ur.UserId == userId
                             select r.NomalizedName).ToList();

            return string.Join(", ", userRoles);

        }

        public bool CheckPassword(int userId, string password)
        {
            var count = _db.Users.Count(m => m.Id == userId && m.Password == password);
            if (count == 0)
            {
                return false;
            }
            else return true;
        }

        public int GetNumberOfUsers()
        {
            return _db.Users.Count();
        }
    }
}