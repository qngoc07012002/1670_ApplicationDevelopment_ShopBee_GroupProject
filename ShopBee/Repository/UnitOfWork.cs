using ShopBee.Data;
using ShopBee.Repository.IRepository;
using ShopBee.Repository;
using ShopBee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBee.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _db;

        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        public IStoreRepository Store { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IUserRepository User { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Book = new BookRepository(_db);
            Store = new StoreRepository(_db);
            Order = new OrderRepository(_db);
            Role = new RoleRepository(_db);
            UserRole = new UserRoleRepository(_db);
            Cart = new CartRepository(_db);
            User = new UserRepository(_db);
            Feedback = new FeedbackRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
       
    }
}
