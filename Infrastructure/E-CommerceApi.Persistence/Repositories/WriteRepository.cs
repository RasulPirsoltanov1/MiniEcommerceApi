using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities.Common;
using E_CommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ECommerceApiDbContext _context;

        public WriteRepository(ECommerceApiDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            return true;
        }

        public async Task RemoveAsync(string id)
        {
            var entity = await Table.FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));
            Table.Remove(entity);
        }

        public bool RemoveRange(List<T> entity)
        {
            Table.RemoveRange(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool UpdateAsync(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
