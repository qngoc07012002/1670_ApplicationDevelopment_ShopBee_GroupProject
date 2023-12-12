namespace ShopBee.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IUserRepository User { get; }
        void Save();

    }
}
