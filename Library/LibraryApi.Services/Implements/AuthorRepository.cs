using LibraryApi.Data.Entities;
using LibraryApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implements
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(int Id, Author author)
        {
            Author authordb=GetById(Id);
            if (authordb != null) 
            {

                authordb.Name = author.Name;
                authordb.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                    
            }
        }
    }
}
