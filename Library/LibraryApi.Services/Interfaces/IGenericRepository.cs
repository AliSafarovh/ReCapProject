using LibraryApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity);

        public List<T> GetAll();

        public T GetById(int id);

        public void Delete(T entity);
    }

}
