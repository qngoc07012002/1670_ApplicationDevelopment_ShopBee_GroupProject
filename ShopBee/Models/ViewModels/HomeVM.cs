namespace ShopBee.Models.ViewModels
{
    public class HomeVM
    {
        public List<Category>? categories {  get; set; }
        
        public List<Book>? books { get; set; }

        public HomeVM()
        {
            categories = new List<Category>();
            books = new List<Book>();
        }
    }
}
