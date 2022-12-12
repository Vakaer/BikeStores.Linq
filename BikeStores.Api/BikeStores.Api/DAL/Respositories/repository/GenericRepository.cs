
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Api.DAL.Respositories.repository
{
    public class GenericRepository : IContract
    {
        private readonly BikeStoresContext _context;

        public GenericRepository(BikeStoresContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
           return await _context.Customers.Take(20).ToListAsync();
        }
    }
}
