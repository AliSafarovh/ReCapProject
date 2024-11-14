using AutoMapper;
using LibraryApi.Data.DTOs.GenreDTO;
using LibraryApi.Data.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreController(IGenreRepository genreRepository,IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        // GET: api/<GenreController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Genre> genres = _genreRepository.GetAll();
            List<GenreGetDTO> data = _mapper.Map<List<GenreGetDTO>>(genres);
            return Ok(data);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Genre genre = _genreRepository.GetById(id);
            if (genre == null) return NotFound();
            GenreGetDTO data = _mapper.Map<GenreGetDTO>(genre);
            return Ok(data);
        }

        // POST api/<GenreController>
        [HttpPost]
        public IActionResult Post([FromForm] GenrePostDTO entity)
        {
            try
            {
                Genre genre = _mapper.Map<Genre>(entity);
                _genreRepository.Add(genre);
                return CreatedAtAction(nameof(Get), new { id = genre.Id }, new
                {
                    Status = "success",
                    Message = "Successfully created",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] GenrePostDTO entity)
        {
            try
            {
                Genre genre = _mapper.Map<Genre>(entity);
                _genreRepository.Update(id, genre);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Genre genre = _genreRepository.GetById(id);
                if (genre == null) return NotFound();
                _genreRepository.Delete(genre);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }
    }
}
