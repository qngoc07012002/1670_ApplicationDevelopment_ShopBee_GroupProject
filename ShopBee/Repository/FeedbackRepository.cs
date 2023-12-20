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

    }
}
