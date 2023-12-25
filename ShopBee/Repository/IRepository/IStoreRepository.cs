using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IStoreRepository : IRepository<Store>
    {
        int GetNumberOfStores();

        public void BecomeASeller(int userId);
    }
}
