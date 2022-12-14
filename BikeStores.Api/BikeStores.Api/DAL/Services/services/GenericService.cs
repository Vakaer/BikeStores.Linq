
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

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

        public async Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct()
        {
           List<OrderCount> orderCounts = await _customerRepository.GetTotalOrdersAgainstEachProduct();
            return orderCounts;
        }
    }
}
