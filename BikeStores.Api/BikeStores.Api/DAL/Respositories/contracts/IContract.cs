﻿using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Respositories.contracts
{
    public interface IContract
    {
        //Task<List<Customer>> GetCustomersAsync();
        //Task<List<BikeStoresViewModel>> GetDataAsync();
        //Task<OrderItem> GetHighestDiscountAsync();
        //Task<List<BikeStoresViewModel>> GetOrderCustomerAndOrderItemsLeftJoin();
        //Task<List<BikeStoresViewModel>> GetProductAndCategoryRightJoin();
        //Task<List<BikeStoresViewModel>> GetProductAndOrderItemsInnerJoin();
        //Task<List<Staff>> GetStaffSelfJoin();
        Task<List<CustomerCount>> GetcustomersCity();
        //Task<List<Order>> GetTotalOrdersAgainstEachProduct();
        //Task<List<BikeStoresViewModel>> GetOrderAgainstEachProduct();

    }
}
