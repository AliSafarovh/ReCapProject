using AutoMapper;
using LibraryApi.Data.DTOs.AuthorDTO;
using LibraryApi.Data.Entities;
using LibraryApi.Services.Implements;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase

    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorRepository authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Author> authors = _authorRepository.GetAll();
            List<AuthorGetDTO> data = _mapper.Map<List<AuthorGetDTO>>(authors);
            return Ok(data);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Author author = _authorRepository.GetById(id);
            if (author == null) return NotFound();
            AuthorGetDTO data = _mapper.Map<AuthorGetDTO>(author);
            return Ok(data);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public IActionResult Post([FromForm] AuthorPostDTO entity) 
        {
            try
            {
                Author author = _mapper.Map<Author>(entity);
                _authorRepository.Add(author);
                return CreatedAtAction(nameof(Get), new { id = author.Id }, new
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

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] AuthorPostDTO entity)
        {
            try
            {
                Author author = _mapper.Map<Author>(entity);
                _authorRepository.Update(id, author);
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

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Author author = _authorRepository.GetById(id);
                if (author == null) return NotFound();
                _authorRepository.Delete(author);
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
    }
}
