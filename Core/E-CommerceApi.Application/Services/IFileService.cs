using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName,string path)>> UploadAsync(IFormFileCollection files, params string[] webPath);
        Task<bool> CopyFileAsync(string path,IFormFile file);
    }
}
