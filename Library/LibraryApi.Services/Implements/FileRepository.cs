using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implements
{
    public class FileRepository : IFileRepository
    {
        public void FileDelete(string rootfolder, string folder, string file)
        {
            string fullPath=Path.Combine(rootfolder,folder,file );
            if (File.Exists(fullPath)) 
            {
                File.Delete(fullPath);
            }   
        }

        public async Task<string> FileUpload(string rootfolder, string folder, IFormFile file)
        {
            string folderPath = Path.Combine(rootfolder, folder);
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string fullPath= Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath)) 
            {
                Directory.CreateDirectory(fullPath);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) 
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
