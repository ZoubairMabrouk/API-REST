using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_EXAMEN_APP.Models.Books
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(50)] // Specifies the maximum length for Name
        public string Name { get; set; }
        [Required]
        public string image { get; set; }

        [Required]
        [MaxLength(50)] // Specifies the maximum length for Bio/Description
        public string Bio { get; set; } // Renamed for clarity
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }

        // Constructor to initialize the collection
        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}
