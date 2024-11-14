using LibraryApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IAuthorRepository:IGenericRepository<Author> 
    {
        public void Update(int Id,Author author);
    }
}
