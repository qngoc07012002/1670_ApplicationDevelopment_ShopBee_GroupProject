namespace ShopBee.Models.ViewModels
{
    public class CartVM
    {
        public List<Cart> carts { get; set; }
        public decimal totalPrice { get; set; }
        public CartVM() {
            carts = new List<Cart>();
            totalPrice = 0;
        }

    }
}
