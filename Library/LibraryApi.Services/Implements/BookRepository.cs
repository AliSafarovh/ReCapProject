using LibraryApi.Data.Entities;
using LibraryApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implements
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(int id, Book book)
        {
            Book bookDb = GetById(id);
            if (bookDb != null)
            {
                bookDb.Name = book.Name;
                bookDb.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
