using BikeStores.Api.ConsoleTable;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;
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
        [HttpGet("GetCustomerFromEachCity")]
        public async Task<ActionResult<List<CustomerCount>>> GetCustomersList() 
        {

            List<CustomerCount> customers = await _customerService.GetCustomersFromEachCityAsync();
            Table tbl = new Table("City", "CustomerCount");
            foreach (var item in customers)
            {
                
                tbl.AddRow(item.State, item.Count);

            }
            tbl.Print();

            return customers;
        }
        [HttpGet("GetCustomerOrderAndOrderItemsLeftJoin")]
        public async Task<ActionResult<List<OrderItemAgainstEachCustomerAndOrder>>> GetOrderCustomerOrderItemsLeftJoin()
        {

            List<OrderItemAgainstEachCustomerAndOrder> customers = await _customerService.GetOrderCustomerAndOrderItemsLeftJoin();
            Table tbl = new Table("fullName", "email", "city", "state", "orderId", "productid", "quantity", "listPrice", "discount","count");

            foreach (var item in customers)
            {

                tbl.AddRow
                    (
                        item.fullName,
                        item.email,
                        item.city,
                        item.state,
                        item.orderId,
                        item.productid,
                        item.quantity,
                        item.listPrice,
                        item.discount,
                        item.orderCount
                        
                    );

            }
            tbl.Print();

            return customers;
        }

        [HttpGet("GetOrderCountAgainstProduct")]
        public async Task<ActionResult<List<OrderCount>>> GetTotalOrdersAgainstProduct()
        {
            List<OrderCount> orders = await _customerService.GetTotalOrdersAgainstEachProduct();
            Table tbl = new Table("ProductId", "ProductName", "OrderCount");

            foreach (OrderCount item in orders)
            {
                tbl.AddRow(item.productId,item.productName, item.OrdersCount);
            }
            tbl.Print();
            return orders;
        }

        [HttpGet("{id:int}", Name = "Method-Id")]
        //[("Adds a new pet using the properties supplied, returns a GUID reference for the pet created.")]
        
        public IActionResult ExecuteMethod([FromBody] int id)
        {
            return Ok(id);
        }
        [HttpGet("{name:string}", Name = "Method-name")]
        //[("Adds a new pet using the properties supplied, returns a GUID reference for the pet created.")]

        public IActionResult ExecuteMethodName([FromBody] string name)
        {
            return Ok(name);
        }
    }
}
