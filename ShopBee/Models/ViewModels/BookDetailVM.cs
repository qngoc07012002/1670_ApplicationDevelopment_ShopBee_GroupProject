namespace ShopBee.Models.ViewModels
{
    public class BookDetailVM
    {
        public Book book { get; set; }
        public List<Feedback> feedbacks { get; set; }

        public BookDetailVM()
        {
            book = new Book();
            feedbacks = new List<Feedback>();
        }
    }
}
