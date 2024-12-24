using API_EXAMEN_APP.Models.Commands;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_EXAMEN_APP.Models.Books
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)] // Assuming max length of 200 for titles
        public string Title { get; set; }

        public string Image { get; set; }

        [MaxLength(1000)] // Assuming max length of 1000 for descriptions
        public string Description { get; set; }

        public string Content { get; set; }


        [Required]
        public DateTime PublishingDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Precision for Price
        public decimal Price { get; set; }

        [Required]
        public int AuthorId { get; set; }

        
        public Author Author { get; set; }

        [Required]
        public int TypeId { get; set; }

        
        public TypeBook Typebook { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        public ICollection<Command> Commands { get; set; }

        // Constructor to initialize collections
        public Book()
        {
            Commands = new HashSet<Command>();
        }
    }
}
