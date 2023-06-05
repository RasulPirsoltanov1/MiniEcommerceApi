using E_CommerceApi.Infrastructure.StaticServices;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Infrastructure.Services.Storage
{
    public class Storage
    {
        public delegate bool HasFile(string pathOrContainer,string filename);
        public async Task<string> FileRenameAsync(string path, string fileName,HasFile hasFile,int count = 1)
        {

            string extension = Path.GetExtension(fileName);
            string newFileName = Path.GetFileNameWithoutExtension(fileName);
            newFileName = NameOperations.CharacterRegulyator(newFileName);
            string fullPath = Path.Combine(path, newFileName) + extension;
            string fullName = newFileName + extension;
            string fullCheckName = newFileName + $"-{count}" + extension;
            if (hasFile(path,fullCheckName))
            {
                count++;
                return await FileRenameAsync(path, fullName,hasFile, count);
            }
            return fullCheckName;
        }
    }
}
