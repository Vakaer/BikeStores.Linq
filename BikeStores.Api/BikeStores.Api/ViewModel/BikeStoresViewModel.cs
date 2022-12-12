using BikeStores.Api.Models;

namespace BikeStores.Api.ViewModel
{
    public class BikeStoresViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public Customer customer { get; set; }

        public int OrderId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }
        public Order order { get; set; }

        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }
        public OrderItem orderItem { get; set; }



        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public Category category { get; set; }

        public string ProductName { get; set; } = null!;
        public int BrandId { get; set; }
        public short ModelYear { get; set; }
        public Product product { get; set; }

        public byte Active { get; set; }
        public int? ManagerId { get; set; }
        public Staff staff { get; set; }

    }
}


