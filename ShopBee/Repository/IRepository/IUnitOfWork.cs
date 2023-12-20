using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IStoreRepository Store { get; }
        IOrderRepository Order { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole{ get; }
        IUserRepository User { get; }

        ICartRepository Cart { get; }

        IFeedbackRepository Feedback{ get; }

        IOrderDetailRepository OrderDetail {  get; }
        void Save();

    }
}
