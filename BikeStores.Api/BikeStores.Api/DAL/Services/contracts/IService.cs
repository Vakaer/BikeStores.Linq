using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Services.contracts
{
    public interface IService
    {
        Task<List<CustomerCount>> GetCustomersFromEachCityAsync();
        Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin();
        Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct();
        Task<List<ProductNamePriceForCategory>> GetProductCategoryRightJoin();
        Task<List<StaffSelfJoin>> GetStaffManagerJoin();
        Task<List<ProductsOrderItemsInnerJoin>> GetProdAndOrdItemsInnerJoin();
        Task<List<decimal>> HighestDiscount(int number);
    }
}
