using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IRepository<Category> Category { get; }
        public IRepository<Book> Book { get; }
        public IRepository<Store> Store { get; }
        public IRepository<Order> Order { get; }
        public IRepository<Role> Role { get; }
        public IRepository<UserRole> UserRole { get; }
        public IUserRepository User { get; }

        void Save();

    }
}
