namespace ShopBee.Models.ViewModels
{
    public class FeedbackVM
    {
        public List<Feedback> Feedbacks { get; set; }
        public FeedbackVM()
        {
            Feedbacks = new List<Feedback>();
        }
    }
}
