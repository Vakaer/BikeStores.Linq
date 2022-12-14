using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Respositories.contracts
{
    public interface IContract
    {
        //Task<List<Customer>> GetCustomersAsync();
        //Task<List<BikeStoresViewModel>> GetDataAsync();
        //Task<OrderItem> GetHighestDiscountAsync();
        Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin();
        Task<List<ProductNamePriceForCategory>> GetProductAndCategoryRightJoin();
        //Task<List<BikeStoresViewModel>> GetProductAndOrderItemsInnerJoin();
        //Task<List<Staff>> GetStaffSelfJoin();
        Task<List<CustomerCount>> GetcustomersCity();
        Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct();
        //Task<List<BikeStoresViewModel>> GetOrderAgainstEachProduct();

    }
}
