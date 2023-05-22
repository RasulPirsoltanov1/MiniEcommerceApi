using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Persistence.Concretes;
using E_CommerceApi.Persistence.Concretes.Customers;
using E_CommerceApi.Persistence.Concretes.Orders;
using E_CommerceApi.Persistence.Concretes.Products;
using E_CommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceService(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddDbContext<ECommerceApiDbContext>(opt =>
            {
                object value = opt.UseSqlServer(Configuration.ConnectionString);
            });
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        }
    }
}
