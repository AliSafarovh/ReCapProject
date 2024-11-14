using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Data.DTOs.BookDTO
{
    public  class BookPostDTO
    {
        public string Name { get; set; }
        public float Count { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public IFormFile? File { get; set; }

    }
}
