using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private DatabaseContext _db;
        public RoleRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Role role)
        {
            _db.Roles.Update(role);
        }
    }
}
