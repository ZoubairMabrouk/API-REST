using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models;
using API_EXAMEN_APP.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesBookController : ControllerBase
    {
        private readonly ApiBbContext _context;

        public TypesBookController(ApiBbContext context)
        {
            _context = context;
        }

        // GET: api/TypeBook
        [HttpGet]
        public async Task<IActionResult> GetAllTypeBooks()
        {
            var typeBooks = await _context.TypeBooks.ToListAsync();
            return Ok(typeBooks);
        }

        // GET: api/TypeBook/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeBookById(int id)
        {
            var typeBook = await _context.TypeBooks.Include(tb => tb.Books).FirstOrDefaultAsync(tb => tb.TypeId == id);
            if (typeBook == null)
            {
                return NotFound("TypeBook not found.");
            }
            return Ok(typeBook);
        }

        // POST: api/TypeBook
        [HttpPost]
        public async Task<IActionResult> CreateTypeBook([FromBody] TypeBookDTO typeBookdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var typeBook = new TypeBook
            {
                Category = typeBookdto.category,
                Description = typeBookdto.desc
            };

            _context.TypeBooks.Add(typeBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTypeBookById), new { id = typeBook.TypeId }, typeBook);
        }

        // PUT: api/TypeBook/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTypeBook(int id, [FromBody] TypeBook updatedTypeBook)
        {
            if (id != updatedTypeBook.TypeId)
            {
                return BadRequest("TypeBook ID mismatch.");
            }

            var typeBook = await _context.TypeBooks.FindAsync(id);
            if (typeBook == null)
            {
                return NotFound("TypeBook not found.");
            }

            typeBook.Category = updatedTypeBook.Category;
            typeBook.Description = updatedTypeBook.Description;
            
            _context.TypeBooks.Update(typeBook);
            await _context.SaveChangesAsync();

            return Ok(typeBook);
        }

        // DELETE: api/TypeBook/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeBook(int id)
        {
            var typeBook = await _context.TypeBooks.Include(tb => tb.Books).FirstOrDefaultAsync(tb => tb.TypeId == id);
            if (typeBook == null)
            {
                return NotFound("TypeBook not found.");
            }

            if (typeBook.Books.Any())
            {
                return BadRequest("Cannot delete TypeBook with associated books.");
            }

            _context.TypeBooks.Remove(typeBook);
            await _context.SaveChangesAsync();

            return Ok("TypeBook deleted successfully.");
        }
    }
}
