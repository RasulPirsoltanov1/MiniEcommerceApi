using E_CommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace E_CommerceApi.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceApiDbContext>
    {
        public ECommerceApiDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommerceApiDbContext> optionsBuilder = new DbContextOptionsBuilder<ECommerceApiDbContext>();
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new ECommerceApiDbContext(optionsBuilder.Options);
        }
    }
}
