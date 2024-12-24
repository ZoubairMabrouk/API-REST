using API_EXAMEN_APP.Models.Books;

namespace API_EXAMEN_APP.Models.Pattern
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<Book> GetBookByName(string name);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book> DeleteBook(int id);

    }
}
