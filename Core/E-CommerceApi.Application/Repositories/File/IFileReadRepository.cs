using E_CommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = E_CommerceApi.Domain.Entities.File;

namespace E_CommerceApi.Application.Repositories
{
    public interface IFileReadRepository: IReadRepository<File>
    {
    }
}
