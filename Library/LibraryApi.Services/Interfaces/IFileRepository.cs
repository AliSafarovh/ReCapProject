using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IFileRepository
    {
        public Task<string> FileUpload(string rootfolder,string folder,IFormFile file);
        public void FileDelete(string rootfolder,string folder,string file);
    }
}
