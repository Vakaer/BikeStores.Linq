using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Respositories.contracts
{
    public interface IContract
    {

        Task<List<decimal>> GetHighestDiscount(int number);
        Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin();
        Task<List<ProductNamePriceForCategory>> GetProductAndCategoryRightJoin();
        Task<List<ProductsOrderItemsInnerJoin>> GetProductAndOrderItemsInnerJoin();
        Task<List<StaffSelfJoin>> GetStaffSelfJoin();
        Task<List<CustomerCount>> GetcustomersCity();
        Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct();
        Task<List<OrderAgainstProductNamePriceID>> OrderForProductNamePriceID ();

    }
}
