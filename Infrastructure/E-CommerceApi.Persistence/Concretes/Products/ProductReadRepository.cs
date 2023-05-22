using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Persistence.Contexts;
using E_CommerceApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Concretes.Products
{
    public class ProductReadRepository : ReadRepository<E_CommerceApi.Domain.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}

