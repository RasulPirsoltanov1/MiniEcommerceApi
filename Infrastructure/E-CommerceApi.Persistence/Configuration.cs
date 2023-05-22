using E_CommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence
{
    internal static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/E-CommerceApi.Api")).AddJsonFile("appsettings.json");
                DbContextOptionsBuilder<ECommerceApiDbContext> optionsBuilder = new DbContextOptionsBuilder<ECommerceApiDbContext>();
                return configurationManager.GetConnectionString("SqlServer");
            }
        }
    }
}
