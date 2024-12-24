using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models;
using API_EXAMEN_APP.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ApiBbContext _context;

        public CommandController(ApiBbContext context)
        {
            _context = context;
        }

        // GET: api/Command
        [HttpGet]
        public async Task<IActionResult> GetAllCommands()
        {
            var commands = await _context.Commands
                                       .ToListAsync();
            return Ok(commands);
        }

        // GET: api/Command/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommandById(int id)
        {
            var command = await _context.Commands
                                        .Include(c => c.User)
                                        .Include(c => c.Book)
                                        .FirstOrDefaultAsync(c => c.Id == id);

            if (command == null)
            {
                return NotFound("Command not found.");
            }

            return Ok(command);
        }

        // POST: api/Command
        [HttpPost]
        public async Task<IActionResult> CreateCommand([FromBody] CommandDTO commanddto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new Command
            {
                UserId = commanddto.UserId,
                BookId = commanddto.BookId,
                Price = commanddto.Price,
                Command_Date = commanddto.Command_Date,
                CommandType = commanddto.CommandType,
            };

            //var bookExists = await _context.Books.AnyAsync(b => b.Id == command.BookId);

            
            _context.Commands.Add(command);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommandById), new { id = command.Id }, command);
        }

        // PUT: api/Command/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommand(int id, [FromBody] Command updatedCommand)
        {
            if (id != updatedCommand.Id)
            {
                return BadRequest("Command ID mismatch.");
            }

            var command = await _context.Commands.FindAsync(id);

            if (command == null)
            {
                return NotFound("Command not found.");
            }

            command.UserId = updatedCommand.UserId;
            command.BookId = updatedCommand.BookId;
            command.CommandType = updatedCommand.CommandType;
            command.Price = updatedCommand.Price;
            command.Command_Date = updatedCommand.Command_Date;

            _context.Commands.Update(command);
            await _context.SaveChangesAsync();

            return Ok(command);
        }

        // DELETE: api/Command/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(int id)
        {
            var command = await _context.Commands.FindAsync(id);

            if (command == null)
            {
                return NotFound("Command not found.");
            }

            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();

            return Ok("Command deleted successfully.");
        }
    }
}
