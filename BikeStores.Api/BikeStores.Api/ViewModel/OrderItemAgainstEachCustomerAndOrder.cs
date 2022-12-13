using BikeStores.Api.Models;

namespace BikeStores.Api.ViewModel
{
    public class OrderItemAgainstEachCustomerAndOrder
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }    
        public string email { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int orderId { get; set; }
        public int orderCount { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
        public decimal listPrice { get; set; }
        public decimal discount { get; set; }
        public Customer customer{ get; set; }
        public Order order { get; set; }
        public OrderItem orderItem { get; set; }
    }
}
