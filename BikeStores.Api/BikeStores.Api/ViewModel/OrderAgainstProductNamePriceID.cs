namespace BikeStores.Api.ViewModel
{
    public class OrderAgainstProductNamePriceID
    {
        public string productName { get; set; }
        public decimal listPrice { get; set; }
        public int productId { get; set; }
        public int orderCount { get; set; }
    }
}
