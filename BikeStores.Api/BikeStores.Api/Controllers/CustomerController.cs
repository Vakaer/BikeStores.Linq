using BikeStores.Api.ConsoleTable;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

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
        public async Task<ActionResult<List<CustomerCount>>> GetCustomerFromEachCity() 
        {

            List<CustomerCount> customers = await _customerService.GetCustomersFromEachCityAsync();
            Table tbl = new Table("City", "CustomerCount");
            foreach (var item in customers)
            {
                
                tbl.AddRow(item.City, item.Count);

            }
            tbl.Print();

            return customers;
        }
        [HttpGet("GetCustomerOrderAndOrderItemsLeftJoin")]
        public async Task<ActionResult<List<OrderItemAgainstEachCustomerAndOrder>>> GetCustomerOrderAndOrderItemsLeftJoin()
        {

            List<OrderItemAgainstEachCustomerAndOrder> customers = await _customerService.GetOrderCustomerAndOrderItemsLeftJoin();
            Table tbl = new Table("fullName", "email", "city", "state", "orderId", "productid", "quantity", "listPrice", "discount");

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
                        item.discount
                        
                    );

            }
            tbl.Print();

            return customers;
        }

        [HttpGet("GetOrderCountAgainstProduct")]
        public async Task<ActionResult<List<OrderCount>>> GetOrderCountAgainstProduct()
        {
            List<OrderCount> orders = await _customerService.GetTotalOrdersAgainstEachProduct();
            Table tbl = new Table("productId", "ProductName", "OrderCount");

            foreach (OrderCount item in orders)
            {
                tbl.AddRow(item.productId, item.productName, item.OrdersCount);
            }
            tbl.Print();
            return orders;
        }

        [HttpGet("GetProductCategoryRightJoin")]
        public async Task<ActionResult<List<ProductNamePriceForCategory>>> GetProductCategoryRightJoin()
        {
            List<ProductNamePriceForCategory> products = await _customerService.GetProductCategoryRightJoin();
            Table tbl = new Table("categoryName","ProductName", "modelYear", "ListPrice");

            foreach (ProductNamePriceForCategory item in products)
            {
                tbl.AddRow(item.categoryName, item.productName, item.modelYear, item.listPrice);
            }
            tbl.Print();
            return products;
        }

        [HttpGet("GetManagerNames")]
        public async Task<ActionResult<List<StaffSelfJoin>>> GetManagerNames()
        {
            List<StaffSelfJoin> managers = await _customerService.GetStaffManagerJoin();
            Table tbl = new Table("StaffName", "ManagerName");

            foreach (StaffSelfJoin item in managers)
            {
                tbl.AddRow(item.staffName, item.managerName);
            }
            tbl.Print();
            return managers;
        }


        [HttpGet("GetPrductNameAndOrderDiscountInnerJoin")]
        public async Task<ActionResult<List<ProductsOrderItemsInnerJoin>>> GetPrductNameAndOrderDiscountInnerJoin()
        {
            List<ProductsOrderItemsInnerJoin> productsOrderItemsInners = await _customerService.GetProdAndOrdItemsInnerJoin();

            Table tbl = new Table("productId", "productName", "listPrice", "orderId", "discount");

            foreach (ProductsOrderItemsInnerJoin item in productsOrderItemsInners)
            {
                tbl.AddRow(item.productId, item.productName, item.listPrice, item.orderId, item.discount);
            }
            tbl.Print();
            return productsOrderItemsInners;
        }




        [HttpGet("GetHighestDiscount/{num}")]
        public async Task<ActionResult<List<decimal>>> GetHighestDiscount(int num)
        {
            List<decimal> highestDiscounts = await _customerService.HighestDiscount(num);

            Table tbl = new Table("discount");

            foreach (decimal highestdiscount in highestDiscounts)
            {
                tbl.AddRow(highestdiscount);
            }


            tbl.Print();
            return Ok(highestDiscounts);
        }

        [HttpGet("OrderAgainstProductNamePriceID")]
        public async Task<List<OrderAgainstProductNamePriceID>> OrderAgainstProductNamePriceID()
        {
            List<OrderAgainstProductNamePriceID> productNamePriceIDs = await _customerService.OrderForEachProductNamePriceID();

            Table tbl = new Table( "productName", "listPrice", "productId", "TotalOrders");

            foreach (OrderAgainstProductNamePriceID prod in productNamePriceIDs)
            {
                tbl.AddRow(prod.productName, prod.listPrice, prod.productId, prod.orderCount);
            }


            tbl.Print();
            return productNamePriceIDs;
        }

        [HttpGet("ExecuteMethod/{id}")]
        //[("Adds a new pet using the properties supplied, returns a GUID reference for the pet created.")]

        public IActionResult ExecuteMethod( int id)
        {
            return Ok(id);
        }
        
    }
}
