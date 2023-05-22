using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Persistence.Contexts;
using E_CommerceApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Concretes.Customers
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
