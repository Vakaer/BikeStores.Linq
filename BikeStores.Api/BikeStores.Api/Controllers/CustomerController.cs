using BikeStores.Api.ConsoleTable;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BikeStores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IService _customerService;

        public CustomerController(ILogger<CustomerController> logger, IService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        [HttpGet("GetList")]
        public async Task<ActionResult<List<Customer>>> GetCustomersList() 
        { 
            List<Customer> customers = await _customerService.GetCustomersAsync();
            Table tbl = new Table("CustomerID", "firstName", "lastName", "Email", "street", "city", "state", "zipCode");
            foreach (var item in customers)
            {
                //Console.WriteLine("Hello "+ item.CustomerId + "   " + item.LastName);
                tbl.AddRow(item.CustomerId, item.FirstName, item.LastName, item.Email, item.Street, item.City, item.State, item.ZipCode);
                
            }
            tbl.Print();

            return customers;
        }
        [HttpGet("{id:int}", Name = "Method-Id")]
        //[("Adds a new pet using the properties supplied, returns a GUID reference for the pet created.")]
        
        public IActionResult ExecuteMethod([FromBody] int id)
        {
            return Ok(id);
        }
    }
}
