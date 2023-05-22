using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Persistence.Contexts;
using E_CommerceApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Concretes.Orders;

public class OrderReadRepository : ReadRepository<E_CommerceApi.Domain.Entities.Order>, IOrderReadRepository
{
    public OrderReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}
