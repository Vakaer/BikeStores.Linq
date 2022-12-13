using BikeStores.Api.Models;

namespace BikeStores.Api.DAL.Services.contracts
{
    public interface IService
    {
        Task<List<Customer>> GetCustomersFromEachCityAsync();
    }
}
