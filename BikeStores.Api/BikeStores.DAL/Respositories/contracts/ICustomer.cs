using BikeStores.Api.Controllers;

namespace BikeStores.Api.DAL.Respositories.contracts
{
    public interface ICustomer
    {
        Task<List<Customer>> GetCustomersAsync();
    }
}
