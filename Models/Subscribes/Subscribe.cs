using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_EXAMEN_APP.Models.Subscribes
{
    public class Subscribe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; }

        public User User { get; set; }

        [Required]
        public int TypeID { get; set; }

        public SubType SubType { get; set; }

        [Required]
        public DateOnly InscriptionAt { get; set; }

        [Required]
        public DateOnly ExpirationAt { get; set; }
    }
}
