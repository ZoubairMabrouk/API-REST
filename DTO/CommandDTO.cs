using API_EXAMEN_APP.Models.Books;
using API_EXAMEN_APP.Models;
using System.ComponentModel.DataAnnotations;

namespace API_EXAMEN_APP.DTO
{
    public class CommandDTO
    {
        public int UserId { get; set; }

        public int BookId { get; set; }
        public string CommandType { get; set; }

        public decimal Price { get; set; }

        public DateTime Command_Date { get; set; }
    }
}
