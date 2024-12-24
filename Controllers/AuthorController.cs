using API_EXAMEN_APP.Models;
using API_EXAMEN_APP.Models.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly ApiBbContext _context; // Assumes a DbContext named AppDbContext

        public AuthorController(ApiBbContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        // GET api/Author/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            return Ok(author);
        }

        // POST api/Author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, author);
        }

        // PUT api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            if (id != updatedAuthor.AuthorId)
            {
                return BadRequest("Author ID mismatch.");
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            author.Name = updatedAuthor.Name;
            author.Bio = updatedAuthor.Bio;

            _context.Authors.Update(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        // DELETE api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            if (author.Books.Any())
            {
                return BadRequest("Cannot delete author with associated books.");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok("Author deleted successfully.");
        }
    }
}
