using System.ComponentModel.DataAnnotations;

namespace API_EXAMEN_APP.Models.Books
{
    public class TypeBook
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(50)] // Specifies a maximum length for the Category
        public string Category { get; set; } // Renamed from Class for clarity

        [Required]
        [MaxLength(250)] // Specifies a maximum length for the Description
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }

        public TypeBook()
        {
            Books = new HashSet<Book>();
        }

    }
}
