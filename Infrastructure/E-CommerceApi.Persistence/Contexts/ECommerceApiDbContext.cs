using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = E_CommerceApi.Domain.Entities.File;

namespace E_CommerceApi.Persistence.Contexts
{
    public class ECommerceApiDbContext : DbContext
    {
        public ECommerceApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<File> Files{ get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (data.State == EntityState.Modified)
                {
                    data.Entity.UpdatedDate = DateTime.UtcNow;
                }
              
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
