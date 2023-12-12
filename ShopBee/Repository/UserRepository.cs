using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
