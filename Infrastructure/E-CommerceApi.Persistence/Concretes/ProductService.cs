using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            return new()
            {
                new()
                {
                    Id=Guid.NewGuid(),
                    Name="dsad",
                    Price=125,
                    CreatedDate=DateTime.Now,
                    Stock=12,
                }
                ,
                   new()
                {
                    Id=Guid.NewGuid(),
                    Name="dsad",
                    Price=123,
                    CreatedDate=DateTime.Now,
                    Stock=12,
                },
                      new()
                {
                    Id=Guid.NewGuid(),
                    Name="dsad",
                    Price=6575,
                    CreatedDate=DateTime.Now,
                    Stock=12,
                },
                         new()
                {
                    Id=Guid.NewGuid(),
                    Name="dsad",
                    Price=3143,
                    CreatedDate=DateTime.Now,
                    Stock=12,
                }
            };
        }
    }
}
