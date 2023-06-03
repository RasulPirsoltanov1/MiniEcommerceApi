using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Persistence.Contexts;
using E_CommerceApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApi.Domain.Entities;
using File = E_CommerceApi.Domain.Entities.File;

namespace E_CommerceApi.Persistence.Concretes.Files
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
