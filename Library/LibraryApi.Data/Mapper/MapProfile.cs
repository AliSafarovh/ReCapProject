using AutoMapper;
using LibraryApi.Data.DTOs.AuthorDTO;
using LibraryApi.Data.DTOs.BookDTO;
using LibraryApi.Data.DTOs.GenreDTO;
using LibraryApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Data.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<BookGetDTO,Book>().ReverseMap();
            CreateMap<Book,BookPostDTO>().ReverseMap();

            CreateMap<Genre,GenreGetDTO>().ReverseMap();
            CreateMap<GenrePostDTO,Genre>().ReverseMap();
            
            CreateMap<Author,AuthorGetDTO>().ReverseMap();
            CreateMap<AuthorPostDTO,Author>().ReverseMap();
        }
    }
}
