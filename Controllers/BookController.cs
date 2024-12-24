using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models.Books;
using API_EXAMEN_APP.Models.Pattern;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _bookRepository;

        // Inject the BookRepository into the controller
        public BookController(IBook bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _bookRepository.GetBooks();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // GET api/<BookController>/name
        [HttpGet("search")]
        public async Task<ActionResult<Book>> GetByName([FromQuery] string name)
        {
            var book = await _bookRepository.GetBookByName(name);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var book = new Book
            {
                Title = bookDTO.Title,
                Image = bookDTO.Image,
                Content = bookDTO.Content,
                Price = bookDTO.price,
                PublishingDate = bookDTO.pubdate,
                LastUpdate = bookDTO.lastupdate,
                Description = bookDTO.description,
                AuthorId = bookDTO.authorid,
                TypeId = bookDTO.typeid,

            };
            var createdBook = await _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            var updatedBook = await _bookRepository.UpdateBook(book);
            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            var book = await _bookRepository.DeleteBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
    }
}
