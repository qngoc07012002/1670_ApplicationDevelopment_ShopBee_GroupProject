namespace ShopBee.Models.ViewModels
{
    public class OrderUserVM
    {
        public List<Order> orders { get; set; }
        public List<List<OrderDetail>> ordersDetail { get; set; }

        public OrderUserVM()
        {
            orders = new List<Order>();
            ordersDetail = new List<List<OrderDetail>>();
        }
    }
}
