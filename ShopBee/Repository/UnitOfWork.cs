using ShopBee.Data;
using ShopBee.Repository.IRepository;
using ShopBee.Repository;

namespace ShopBee.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _db;
        public ICategoryRepository Category { get; private set; }
        public UnitOfWork(DatabaseContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
