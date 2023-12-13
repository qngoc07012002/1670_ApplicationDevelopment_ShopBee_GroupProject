using ShopBee.Data;
using ShopBee.Repository.IRepository;
using ShopBee.Repository;
using ShopBee.Models;

namespace ShopBee.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _db;

        public IRepository<Category> Category { get; private set; }
        public IRepository<Book> Book { get; private set; }
        public IRepository<Store> Store { get; private set; }
        public IRepository<Order> Order { get; private set; }
        public IRepository<Role> Role { get; private set; }
        public IRepository<UserRole> UserRole { get; private set; }
        public IRepository<Cart> Cart { get; private set; }
        public IUserRepository User { get; private set; }

        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
            Category = new Repository<Category>(_db);
            Book = new Repository<Book>(_db);
            Store = new Repository<Store>(_db);
            Order = new Repository<Order>(_db);
            Role = new Repository<Role>(_db);
            UserRole = new Repository<UserRole>(_db);
            Cart = new Repository<Cart>(_db);


            //Options
            User = new UserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
