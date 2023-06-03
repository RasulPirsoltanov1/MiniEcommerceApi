using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Abstractions
{
    public interface IStorage
    {
        Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, params string[] webPath);
        Task DeleteAsync(string pathOrContainer,string fileName);
        List<string> GetFiles(string pathOrContainer);
        bool HasFile(string pathOrContainer, string fileName);
    }
}
