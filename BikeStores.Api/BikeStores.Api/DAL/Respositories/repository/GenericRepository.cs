
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.Models;
using BikeStores.Api.ViewModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.RegularExpressions;
using BikeStores.Api.ViewModel;

namespace BikeStores.Api.DAL.Respositories.repository
{
   

    public class GenericRepository : IContract
    {
        private readonly BikeStoresContext _context;

        public GenericRepository(BikeStoresContext context)
        {
            _context = context;
        }
        

        //SELECT city , COUNT(*)
        // as TotalCustomers
        //FROM sales.customers
        //GROUP BY city
        public async Task<List<CustomerCount>> GetcustomersCity()
        {
            var finalCustomerslist = await _context.Customers.GroupBy(p => p.State)
                                           .Select(p => new CustomerCount { State = p.Key, Count = p.Count() })
                                           .ToListAsync();
            return finalCustomerslist;
        }


        //2-- Which order is of which customer using LEFT JOIN on 3 Tables

        //SELECT CONCAT(c.first_name , ' ' , c.last_name) as CustomerName, c.email, c.city, c.state , o.order_id 
        //, T.product_id, T.quantity, T.list_price, T.discount

        //FROM sales.customers c
        //LEFT JOIN sales.orders o
        //ON o.customer_id = c.customer_id
        //LEFT JOIN sales.order_items T
        //ON T.order_id = o.order_id
        //order By CustomerName
        public async Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin()
        {
            IQueryable<string> CustomerName = (from cust in _context.Customers
                                               select cust.FirstName)
                                              .Concat
                                              (from cust in _context.Customers
                                               select cust.LastName);



            IQueryable<OrderItemAgainstEachCustomerAndOrder> result = from cust in _context.Customers
                         join order in _context.Orders on cust.CustomerId equals order.OrderId into orders
                         from order in orders.DefaultIfEmpty()
                         let oCount =
                         (
                             from o in _context.Orders
                             where o.OrderId == order.OrderId
                             select o
                         ).Count()
                         let fullname = cust.FirstName + " " + cust.LastName
                         join ordIt in _context.OrderItems on order.OrderId equals ordIt.OrderId into orderItems
                         from ordIt in orderItems.DefaultIfEmpty()
                         orderby fullname
                            
                         
                         select new OrderItemAgainstEachCustomerAndOrder
                         {
                             fullName = fullname,
                             email = cust.Email,
                             city = cust.City,
                             state = cust.State,
                             orderId = order.OrderId,
                             orderCount = oCount,
                             productid = ordIt.ProductId,
                             quantity = ordIt.Quantity,
                             listPrice = ordIt.ListPrice,
                             discount = ordIt.Discount,
                             customer = cust,
                             order = order,
                             orderItem = ordIt


                         };

            List<OrderItemAgainstEachCustomerAndOrder> customerAndOrders = result.Take(100).ToList();

            return customerAndOrders;
                                  
        }

       

        
    }
}
