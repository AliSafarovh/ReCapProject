using LibraryApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IGenreRepository:IGenericRepository<Genre>
    {
        public void Update(int id, Genre genre);    
    }
}
