
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

namespace BikeStores.Api.DAL.Respositories.repository
{
    public class GenericRepository : IContract
    {
        private readonly BikeStoresContext _context;

        public GenericRepository(BikeStoresContext context)
        {
            _context = context;
        }
        //public Task<List<Customer>> GetCustomersAsync()
        //{
        //    return Task.FromResult(new List<Customer>());
        //}    
        //public Task<List<BikeStoresViewModel>> GetDataAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //SELECT city , COUNT(*)
        // as TotalCustomers
        //FROM sales.customers
        //GROUP BY city
        public async Task<List<Customer>> GetcustomersCity()
        {
            var customers =  _context.Customers.ToList();
            //var customerCount = customers.Count();
            //var cityGroup = customers.GroupBy(x => x.City);
            //var finalCustomers = (from c in customers
            //                      select new
            //                      {
            //                          c.City,
            //                          c.CustomerId
            //                      }).ToList();
            var finalCustomerslist =_context.Customers.GroupBy(p => p.City).Select(p => new { City = p.Key, Count = p.Count() });
            return customers;
            


        }

        //1-- Write a query to find the Nth highest salary from the table without using TOP/limit keyword.
        //Select Distinct s1.discount
        //from sales.order_items s1 WHERE 3-1 = 
        //(Select COUNT(Distinct s2.discount) 
        //From sales.order_items s2
        //WHERE s1.discount<s2.discount)
        public Task<OrderItem> GetHighestDiscountAsync()
        {
            throw new NotImplementedException();
        }

        

        public Task<List<BikeStoresViewModel>> GetOrderAgainstEachProduct()
        {
            throw new NotImplementedException();
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
        public Task<List<BikeStoresViewModel>> GetOrderCustomerAndOrderItemsLeftJoin()
        {
            throw new NotImplementedException();
        }

        //SELECT c.category_name, p.product_name, p.model_year, p.list_price
        //FROM production.categories c
        //RIGHT JOIN production.products p
        //ON p.category_id = c.category_id
        public Task<List<BikeStoresViewModel>> GetProductAndCategoryRightJoin()
        {
            throw new NotImplementedException();
        }

        //SELECT *
        //FROM production.products
        //INNER JOIN sales.order_items
        //ON sales.order_items.product_id = production.products.product_id
        public Task<List<BikeStoresViewModel>> GetProductAndOrderItemsInnerJoin()
        {
            throw new NotImplementedException();
        }

        //SELECT a.staff_id AS "Staff_ID",a.first_name AS "Staff Name",
        //b.staff_id AS "Manager ID", b.first_name AS "Manager Name"
        //FROM sales.staffs a, sales.staffs b
        //WHERE a.manager_id = b.staff_id;
        public Task<List<Staff>> GetStaffSelfJoin()
        {
            throw new NotImplementedException();
        }

        //SELECT p.product_name, p.list_price, p.product_id, COUNT(*)
        //AS ORDERS
        //FROM production.products p
        //LEFT OUTER JOIN sales.order_items o
        //ON o.product_id = p.product_id
        //Where p.product_name IN ('Ritchey Timberwolf Frameset - 2016','Trek Fuel EX 8 29 - 2016')
        //GROUP BY p.product_id,p.product_name,p.list_price
        //HAVING COUNT(*)
        // BETWEEN 50 AND 99
        public Task<List<Order>> GetTotalOrdersAgainstEachProduct()
        {
            throw new NotImplementedException();
        }
    }
}
