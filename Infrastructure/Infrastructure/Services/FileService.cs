using E_CommerceApi.Application.Services;
using E_CommerceApi.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace E_CommerceApi.Infrastructure.Services;

public class FileService : IFileService
{
    private IWebHostEnvironment _webHostEnvironment { get; set; }

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
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
        catch(Exception ex)
        {
            //todo log
            throw ex;
        }
    }

    private async Task<string> FileRenameAsync(string path,string fileName,int count=1)
    {

        string extension=Path.GetExtension(fileName);
        string newFileName=Path.GetFileNameWithoutExtension(fileName);
        newFileName = NameOperations.CharacterRegulyator(newFileName);
        string fullPath = Path.Combine(path,newFileName)+extension;
        string fullName = newFileName + extension;
        string fullCheckName= newFileName + $"-{count}" + extension;
        if (File.Exists(Path.Combine(path,fullCheckName)))
        {
            count++;
            return await FileRenameAsync(path,fullName,count);
        }
        return fullCheckName;
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, params string[] webPath)
    {
        List<(string fileName, string path)> datas = new();
        List<bool> results=new();
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
            string newFileName = await FileRenameAsync(uploadPath,file.FileName);
            bool result=await CopyFileAsync(Path.Combine(uploadPath, newFileName), file);
            datas.Add((newFileName,Path.Combine(uploadPath, newFileName)));
            results.Add(result);
        }
        if (results.TrueForAll(r => r.Equals(true)))
        {
            return datas;   
        }
        return null;
    }
}
