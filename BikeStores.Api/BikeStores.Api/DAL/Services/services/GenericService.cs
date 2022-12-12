
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;

namespace BikeStores.Api.DAL.Services.services
{
    public class GenericService : IService
    {
        private readonly IContract _customerRepository;

        public GenericService(IContract customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> customerEntities = await _customerRepository.GetCustomersAsync();
            return customerEntities;
        }

    }
}
