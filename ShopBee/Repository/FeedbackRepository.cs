using Microsoft.EntityFrameworkCore;
using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Repository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private DatabaseContext _db;
        public FeedbackRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }
        public List<Feedback> GetFeedbackByBook(int bookId)
        {
            var query = _db.Feedbacks.Where(c => c.BookId == bookId);
            string includeProperties = "User";
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return query.ToList();
        }
    }
}
