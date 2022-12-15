
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;
using System.Collections.Generic;

namespace BikeStores.Api.DAL.Services.services
{
    public class GenericService : IService
    {
        private readonly IContract _customerRepository;

        public GenericService(IContract customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<List<CustomerCount>> GetCustomersFromEachCityAsync()
        {
            List<CustomerCount> customerEntities = await _customerRepository.GetcustomersCity();
            return customerEntities;
        }

        public async Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin()
        {
            List<OrderItemAgainstEachCustomerAndOrder> customerAndOrders = await _customerRepository.GetOrderCustomerAndOrderItemsLeftJoin();
            return customerAndOrders;
        }

        public async Task<List<ProductsOrderItemsInnerJoin>> GetProdAndOrdItemsInnerJoin()
        {
            List<ProductsOrderItemsInnerJoin> productsOrderItems = await _customerRepository.GetProductAndOrderItemsInnerJoin();
            return productsOrderItems;
        }

        public async Task<List<ProductNamePriceForCategory>> GetProductCategoryRightJoin()
        {
            List<ProductNamePriceForCategory> productNamePriceForCategories = await _customerRepository.GetProductAndCategoryRightJoin();
            return productNamePriceForCategories;
        }

        public async Task<List<StaffSelfJoin>> GetStaffManagerJoin()
        {
            List<StaffSelfJoin> joins = await _customerRepository.GetStaffSelfJoin();
            return joins;
        }

        public async Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct()
        {
           List<OrderCount> orderCounts = await _customerRepository.GetTotalOrdersAgainstEachProduct();
            return orderCounts;
        }

        public async Task<List<decimal>> HighestDiscount(int number)
        {
            List<decimal> highestDiscounts = await _customerRepository.GetHighestDiscount(number);
            return highestDiscounts;
        }

        public async Task<List<OrderAgainstProductNamePriceID>> OrderForEachProductNamePriceID()
        {
            List<OrderAgainstProductNamePriceID> orderAgainstProducts = await _customerRepository.OrderForProductNamePriceID();
            return orderAgainstProducts;
        }
    }
}
