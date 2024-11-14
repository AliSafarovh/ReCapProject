using LibraryApi.Data.Entities;
using LibraryApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implements
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(int id, Genre genre)
        {
            Genre genreDb=GetById(id);
            if (genreDb != null)
            {
                genreDb.Name = genre.Name;
                genreDb.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
