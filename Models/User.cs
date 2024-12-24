using API_EXAMEN_APP.Models.Subscribes;
using API_EXAMEN_APP.Models.Commands;
using Microsoft.AspNetCore.Identity;


namespace API_EXAMEN_APP.Models
{
    public class User : IdentityUser
    { 

        public ICollection<Command> Commands { get; set; }
        public ICollection<Subscribe> Subscribes { get; set; }
    }
}
