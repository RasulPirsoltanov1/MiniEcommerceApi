using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities.Common;
using E_CommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceApiDbContext _context;

        public ReadRepository(ECommerceApiDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await Table.FirstOrDefaultAsync(p=>p.Id==Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            return await Table.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
