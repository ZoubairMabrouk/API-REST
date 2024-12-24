using API_EXAMEN_APP.DTO;
using System.ComponentModel.DataAnnotations;

namespace API_EXAMEN_APP.Models.Subscribes
{
    public class SubType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] // Limit the length of the Name
        public string Name { get; set; }

        [Required]
        [MaxLength(250)] // Limit the length of the Description
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)] // Ensure a valid price range
        public decimal Price { get; set; }

        public ICollection<Subscribe> Subscribes { get; set; }

        // Constructor to initialize navigation properties
        public SubType()
        {
            Subscribes = new HashSet<Subscribe>();
        }
    }
}
