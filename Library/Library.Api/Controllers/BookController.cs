using AutoMapper;
using LibraryApi.Data.DTOs.BookDTO;
using LibraryApi.Data.Entities;
using LibraryApi.Data.Mapper;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        IWebHostEnvironment _env;
        public BookController(IFileRepository fileRepository,IBookRepository bookRepository, IMapper mapper,IWebHostEnvironment env)
        {
            _fileRepository = fileRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _env = env;
        }
        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Book> books = _bookRepository.GetAll();
            List<BookGetDTO> data = _mapper.Map<List<BookGetDTO>>(books);

            return Ok(data);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _bookRepository.GetById(id);
            if (book is null) NotFound();
            BookGetDTO data = _mapper.Map<BookGetDTO>(book);

            return Ok(data);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task <IActionResult> Post([FromForm] BookPostDTO entity)
        {
            try
            {
                Book book = _mapper.Map<Book>(entity);
                if(entity.File != null)
                {
                    book.Image = await _fileRepository.FileUpload(_env.WebRootPath, "books", entity.File);
                }

                _bookRepository.Add(book);
                return CreatedAtAction(nameof(Get), new
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
                    Detail = ex.InnerException?.Message 
                });
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, [FromForm] BookPostDTO entity)
        {
            try
            {
                Book bookDb = _bookRepository.GetById(id);
                if (bookDb is null) return NotFound();
                Book book = _mapper.Map<Book>(entity);
                if (entity.File != null)
                {
                    _fileRepository.FileDelete(_env.WebRootPath, "books", bookDb.Image);
                    book.Image = await _fileRepository.FileUpload(_env.WebRootPath, "books", entity.File);
                }
                _bookRepository.Update(id, book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = "Error",
                    Message = ex.Message
                });
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Book book = _bookRepository.GetById(id);
                if (book is null) return NotFound();
                _bookRepository.Delete(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = "Error",
                    Message = ex.Message
                });

            }
        }
    }
}
