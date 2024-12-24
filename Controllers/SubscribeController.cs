using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models;
using API_EXAMEN_APP.Models.Subscribes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ApiBbContext _context;

        public SubscribeController(ApiBbContext context)
        {
            _context = context;
        }

        // GET: api/Subscribe
        [HttpGet]
        public async Task<IActionResult> GetAllSubscribes()
        {
            var subscribes = await _context.Subscribes
                                           .Include(s => s.User)        // Inclure les utilisateurs
                                           .Include(s => s.SubType)     // Inclure les types d'abonnement
                                           .ToListAsync();

            return Ok(subscribes);
        }

        // GET: api/Subscribe/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscribeById(int id)
        {
            var subscribe = await _context.Subscribes
                                          .Include(s => s.User)        // Inclure les utilisateurs
                                          .Include(s => s.SubType)     // Inclure les types d'abonnement
                                          .FirstOrDefaultAsync(s => s.Id == id);

            if (subscribe == null)
            {
                return NotFound("Subscribe not found.");
            }

            return Ok(subscribe);
        }

        // POST: api/Subscribe
        [HttpPost]
        public async Task<IActionResult> CreateSubscribe([FromBody] SubscribeDTO subscribeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subscribe = new Subscribe
            {
                UserID = subscribeDTO.UserId,
                InscriptionAt = subscribeDTO.InscriptionAt,
                ExpirationAt = subscribeDTO.ExpirationAt,
                TypeID = subscribeDTO.SubTypeId
            };

            _context.Subscribes.Add(subscribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubscribeById), new { id = subscribe.Id }, subscribe);
        }

        // PUT: api/Subscribe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscribe(int id, [FromBody] Subscribe updatedSubscribe)
        {
            if (id != updatedSubscribe.Id)
            {
                return BadRequest("Subscribe ID mismatch.");
            }

            var subscribe = await _context.Subscribes.FindAsync(id);

            if (subscribe == null)
            {
                return NotFound("Subscribe not found.");
            }

            subscribe.UserID = updatedSubscribe.UserID;
            subscribe.TypeID = updatedSubscribe.TypeID;
            subscribe.InscriptionAt = updatedSubscribe.InscriptionAt;
            subscribe.ExpirationAt = updatedSubscribe.ExpirationAt;

            _context.Subscribes.Update(subscribe);
            await _context.SaveChangesAsync();

            return Ok(subscribe);
        }

        // DELETE: api/Subscribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribe(int id)
        {
            var subscribe = await _context.Subscribes.FindAsync(id);

            if (subscribe == null)
            {
                return NotFound("Subscribe not found.");
            }

            _context.Subscribes.Remove(subscribe);
            await _context.SaveChangesAsync();

            return Ok("Subscribe deleted successfully.");
        }
    }
}
