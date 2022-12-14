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
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
            List<CustomerCount> finalCustomerslist = await _context.Customers.GroupBy(p => p.State)
                                           .Select(p => new CustomerCount { State = p.Key, Count = p.Count() })
                                           .ToListAsync();
            return finalCustomerslist;
        }


        //2-- Which order is of which customer using LEFT JOIN on 3 Tables

        //select concat(c.first_name , ' ' , c.last_name) as customername, c.email, c.city, c.state , o.order_id 
        //, t.product_id, t.quantity, t.list_price, t.discount

        //from sales.customers c
        //left join sales.orders o
        //on o.customer_id = c.customer_id
        //left join sales.order_items t
        //on t.order_id = o.order_id
        //order by customername
        public async Task<List<OrderItemAgainstEachCustomerAndOrder>> GetOrderCustomerAndOrderItemsLeftJoin()
        {
            //IQueryable<string> CustomerName = (from cust in _context.Customers
            //                                   select cust.FirstName)
            //                                  .Concat
            //                                  (from cust in _context.Customers
            //                                   select cust.LastName);



            IQueryable<OrderItemAgainstEachCustomerAndOrder> result = (from cust in _context.Customers
                                                                      join order in _context.Orders on cust.CustomerId equals order.CustomerId into orders
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
                                                                          firstName = cust.FirstName,
                                                                          lastName = cust.LastName,
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


                                                                      }).Take(100);
                         

            List<OrderItemAgainstEachCustomerAndOrder> customerAndOrders =await  result.ToListAsync();

            return customerAndOrders;

        }

        //SELECT  c.category_name, p.product_name, p.model_year, p.list_price
        //FROM production.categories c
        //RIGHT JOIN production.products p
        //ON p.category_id = c.category_id
        public async Task<List<ProductNamePriceForCategory>> GetProductAndCategoryRightJoin()
        {
            List<ProductNamePriceForCategory> query = await (from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.CategoryId into joined
                        from j in joined.DefaultIfEmpty()
                        select new ProductNamePriceForCategory
                        {
                            categoryName = j.CategoryName,
                            productName = p.ProductName,
                            modelYear = p.ModelYear,
                            listPrice = p.ListPrice
                        }).Take(20).ToListAsync();
            return query;
        }



        //SELECT product_id, COUNT(*)
        // as TotalOrdersForEachProduct
        //FROM sales.order_items
        //GROUP BY product_id
        public async Task<List<OrderCount>> GetTotalOrdersAgainstEachProduct()
        {
            var ordersCount = _context.OrderItems
                                           .GroupBy(p => p.ProductId)
                                           .Select( group => new OrderCount
                                           {
                                               productId = group.Key,
                                               OrdersCount = group.Count()
                                              
                                           })
                                           .OrderBy(p => p.productId).ToList();
            List<OrderCount> orderCountsList = (from p in _context.Products
                                 join ordIt in _context.OrderItems on p.ProductId equals ordIt.ProductId into productItems
                                 from ordIt in productItems.DefaultIfEmpty()
                                
                                   group p by p.ProductName into grp
                                   where grp.Count() < 10 
                                   select new OrderCount
                                 {
                                    
                                     productName = grp.Key,
                                     OrdersCount = grp.Count()
                                     

                                 }).ToList();


             
            return orderCountsList;
                                           
                                           
        }

        //SELECT a.first_name AS "Staff Name",
        //b.first_name AS "Manager Name"
        //FROM sales.staffs a, sales.staffs b
        //WHERE a.manager_id = b.staff_id;
        public async Task<List<StaffSelfJoin>> GetStaffSelfJoin()
        {
            List<StaffSelfJoin> SelfJoinQuery =await (from s in _context.Set<Staff>()
                                join s1 in _context.Set<Staff>() on s.StaffId equals s1.ManagerId
                                select new StaffSelfJoin
                                {
                                    staffName = s.FirstName + " " + s.LastName,
                                    managerName = s1.FirstName + " " + s.LastName,
                                }).ToListAsync();
            return SelfJoinQuery;
        }


        //SELECT p.product_id,p.product_name, o.list_price,o.discount
        //FROM production.products As p
        //INNER JOIN sales.order_items As o
        //ON p.product_id = o.product_id
        public async Task<List<ProductsOrderItemsInnerJoin>> GetProductAndOrderItemsInnerJoin()
        {
            List<ProductsOrderItemsInnerJoin> innerJoinQuery = await (from p in _context.Set<Product>()
                                  join o in _context.Set<OrderItem>() on p.ProductId equals o.ProductId
                                  select new ProductsOrderItemsInnerJoin
                                  {
                                      productId = p.ProductId,
                                      productName = p.ProductName,
                                      listPrice = p.ListPrice,
                                      orderId = o.OrderId,
                                      discount = o.Discount

                                  }).Take(20).ToListAsync();
            return innerJoinQuery;
        }


        //Select Distinct s1.discount
        //from sales.order_items s1 WHERE 2-1 = 
        //(Select COUNT(Distinct s2.discount)
        //From sales.order_items s2
        //WHERE s1.discount<s2.discount)



        public async Task<List<HighestDiscount>> GetHighestDiscountAsync(int number)
        {
            List<HighestDiscount> query = await (from o in _context.OrderItems
                                                 group o by o.Discount into gr
                                                 select new HighestDiscount
                                                 {
                                                     discount = gr.OrderByDescending(d => d.Discount)
                                                                .Distinct()
                                                                .Skip(number - 1)
                                                                .FirstOrDefault()
                                                                .Discount

                                                 }).ToListAsync();

            //var query = await (from s1 in _context.OrderItems where 1 >
            //                                     (from s2 in _context.OrderItems where 
            //                                      s2.Discount < s1.Discount select s2.Discount).Distinct().Count() 
            //                                     select s1.Discount).Distinct().ToListAsync();
            return query;
        }
//        Select Distinct s1.discount
//from sales.order_items s1 WHERE 1-1= 
//        (Select COUNT(Distinct s2.discount)
//        From sales.order_items s2
//        WHERE s1.discount<s2.discount)


        public async Task<List<decimal>> GetHighestDiscount(int number)
        {
            /*var alternateQuery = await (from o in _context.OrderItems
                                        group o by o.Discount into grp
                                        let highdesc = (grp.OrderByDescending(d => d.Discount).Distinct().Skip(number - 1).FirstOrDefault().Discount)
                                        where grp.Key == highdesc
                                        select grp.Key
                                  ).Distinct().ToListAsync()*/;

           List<decimal> query = await (from s1 in _context.OrderItems
                           where number - 1 ==
                                                 (from s2 in _context.OrderItems
                                                  where

                                                  s2.Discount > s1.Discount
                                                  select s2.Discount
                                                   ).Distinct().Count()
                           select s1.Discount ).Distinct().ToListAsync();

            return query;
        }
    }
}
