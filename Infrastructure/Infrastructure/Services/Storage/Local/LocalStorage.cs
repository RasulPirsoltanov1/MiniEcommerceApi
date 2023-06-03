using E_CommerceApi.Application.Abstractions.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private IWebHostEnvironment _webHostEnvironment;


        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete(Path.Combine(path,fileName));
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            List<string> fileNames = directory.GetFiles().Select(f => f.Name).ToList();
            return fileNames;
        }

        public bool HasFile(string path, string fileName)
        {
            return File.Exists(Path.Combine(path, fileName));
        }
        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                //todo log
                throw ex;
            }
        }
        public async Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, params string[] webPath)
        {
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            string uploadPath = _webHostEnvironment.WebRootPath;
            foreach (var path in webPath)
            {
                uploadPath = Path.Combine(uploadPath, path);
            }
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            foreach (var file in files)
            {
                bool result = await CopyFileAsync(Path.Combine(uploadPath, file.Name), file);
                datas.Add((file.Name, Path.Combine(uploadPath, file.Name)));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            return null;
        }
    }
}
