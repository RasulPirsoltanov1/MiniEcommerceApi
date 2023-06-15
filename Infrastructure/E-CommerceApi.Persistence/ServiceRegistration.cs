using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities.Identity;
using E_CommerceApi.Persistence.Concretes;
using E_CommerceApi.Persistence.Concretes.Customers;
using E_CommerceApi.Persistence.Concretes.Files;
using E_CommerceApi.Persistence.Concretes.InvoiceFiles;
using E_CommerceApi.Persistence.Concretes.Orders;
using E_CommerceApi.Persistence.Concretes.ProductImageFiles;
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
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository,FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository,InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ECommerceApiDbContext>();
        }
    }
}
