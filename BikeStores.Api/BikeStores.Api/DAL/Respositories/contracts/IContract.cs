using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Respositories.contracts
{
    public interface IContract
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<List<BikeStoresViewModel>> GetDataAsync();

    }
}
