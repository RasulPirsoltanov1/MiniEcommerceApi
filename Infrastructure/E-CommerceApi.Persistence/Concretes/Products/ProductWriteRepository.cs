using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Persistence.Concretes.Customers;
using E_CommerceApi.Persistence.Contexts;
using E_CommerceApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Concretes.Products;
public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
{
    public ProductWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}
