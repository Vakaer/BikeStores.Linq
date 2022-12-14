namespace BikeStores.Api.ViewModel
{
    public class ProductsOrderItemsInnerJoin
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int orderId { get; set; }
        public decimal listPrice { get; set; }
        public decimal discount { get; set; }
    }
}
