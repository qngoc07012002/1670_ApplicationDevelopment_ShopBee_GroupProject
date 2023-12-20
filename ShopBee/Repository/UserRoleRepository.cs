using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private DatabaseContext _db;
        public UserRoleRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

    }
}
