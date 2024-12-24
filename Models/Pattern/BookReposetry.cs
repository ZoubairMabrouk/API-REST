using API_EXAMEN_APP.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Models.Pattern
{
    public class BookRepository : IBook
    {
        private readonly ApiBbContext _context;

        public BookRepository(ApiBbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Typebook)
                .FirstOrDefaultAsync(b => b.Title.Contains(name));
        }

        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }
    }
}
