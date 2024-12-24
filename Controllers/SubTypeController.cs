using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models;
using API_EXAMEN_APP.Models.Subscribes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTypeController : ControllerBase
    {
        private readonly ApiBbContext _context;

        public SubTypeController(ApiBbContext context)
        {
            _context = context;
        }

        // GET: api/SubType
        [HttpGet]
        public async Task<IActionResult> GetAllSubTypes()
        {
            var subTypes = await _context.Subtypes.ToListAsync();
            return Ok(subTypes);
        }

        // GET: api/SubType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubTypeById(int id)
        {
            var subType = await _context.Subtypes.FindAsync(id);

            if (subType == null)
            {
                return NotFound("SubType not found.");
            }

            return Ok(subType);
        }

        // POST: api/SubType
        [HttpPost]
        public async Task<IActionResult> CreateSubType([FromBody] SubTupeDTO subTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subType = new SubType
            {
                Name = subTypeDTO.Name,
                Description = subTypeDTO.Description,
                Price = subTypeDTO.Price
            };

            _context.Subtypes.Add(subType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubTypeById), new { id = subType.Name }, subType);
        }

        // PUT: api/SubType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubType(int id, [FromBody] SubType updatedSubType)
        {
            if (id != updatedSubType.Id)
            {
                return BadRequest("SubType ID mismatch.");
            }

            var subType = await _context.Subtypes.FindAsync(id);

            if (subType == null)
            {
                return NotFound("SubType not found.");
            }

            subType.Name = updatedSubType.Name;
            subType.Description = updatedSubType.Description;
            subType.Price = updatedSubType.Price;

            _context.Subtypes.Update(subType);
            await _context.SaveChangesAsync();

            return Ok(subType);
        }

        // DELETE: api/SubType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubType(int id)
        {
            var subType = await _context.Subtypes.FindAsync(id);

            if (subType == null)
            {
                return NotFound("SubType not found.");
            }

            _context.Subtypes.Remove(subType);
            await _context.SaveChangesAsync();

            return Ok("SubType deleted successfully.");
        }
    }
}
