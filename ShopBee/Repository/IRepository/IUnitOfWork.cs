namespace ShopBee.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IBookRepository Book { get; }
        public IStoreRepository Store { get; }
        public IOrderRepository Order { get; }
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }
        public ICartRepository Cart { get; }

        void Save();


    }
}
