
using BikeStores.Api.DAL.Respositories;
using BikeStores.Api.DAL.Respositories.contracts;
using BikeStores.Api.DAL.Respositories.repository;
using BikeStores.Api.DAL.Services.contracts;
using BikeStores.Api.DAL.Services.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BikeStores.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://*:5252");

            // AddCors
            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://*:5252")
                                        .AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
            });
            

            //repositories
            builder.Services.AddScoped<IContract, GenericRepository>();
           

            //services
            builder.Services.AddScoped<IService, GenericService>();
            

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<BikeStoresContext>(
            options => options.UseSqlServer("name=ConnectionStrings:Default"));
            
            builder.Services.AddControllers()
                            .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EF LinQ Api",
                    Description = "\n" +
                    "An ASP.NET Core Web API for performing different Queries\n<br>" +
                    "Press respective key to perform following operations\n" +
                    "1: Write a query to find the 2nd highest discount from the order table without using TOP/limit keyword.\n" +
                    "2: Which order is of which customer using LEFT JOIN on 3 Tables\n" +
                    "3: RIGHT JOIN\n" +
                    "4: INNER JOIN\n" +
                    "5: SELF JOIN\n" +
                    "6: SHOWS NUMBER OF CUSTOMERS FROM EACH CITY\n" +
                    "7: SHOWS NUMBER OF ORDERS AGAINST EACH PRODUCT ID\n" +
                    "8:  SHOWS NUMBER OF ORDERS AGAINST EACH PRODUCT NAME, LIST_PRICE & PRODUCT_ID\n",
                    TermsOfService = new Uri("https://example.com/terms"),
                   
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);
            app.MapControllers();

            app.Run();



        }
    }
}