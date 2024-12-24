using API_EXAMEN_APP.Models.Books;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_EXAMEN_APP.Models.Commands
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation property for User (if applicable)
        public User User { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        [MaxLength(50)] // Limit the length of CommandType
        public string CommandType { get; set; } // Fixed typo

        [Required]
        [Range(0, double.MaxValue)] // Ensure a valid price range
        public decimal Price { get; set; }

        [Required]
        public DateTime Command_Date { get; set; }
    }
}
