using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Data.Entities
{
    public class Book:BaseEntity
    {
        public  int BookId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Count { get; set; }
        public int GenreId { get; set; }
        public Genre Genre {  get; set; }
        public int AuthorId { get; set; }
        public Author Author {  get; set; }
    }
}
