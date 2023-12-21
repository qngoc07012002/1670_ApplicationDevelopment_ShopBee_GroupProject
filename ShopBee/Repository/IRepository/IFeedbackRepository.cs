using ShopBee.Models;

namespace ShopBee.Repository.IRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        public List<Feedback> GetFeedbackByBook(int bookId);
    }
}
