using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractibleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InteractibleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataInteractible>> Get()
        {
            return Ok(_context.Interactibles.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractible> Get(int id)
        {
            var interactible = _context.Interactibles
                .FirstOrDefault(i => i.InteractibleID == id);

            if (interactible == null)
            {
                return NotFound();
            }

            return Ok(interactible);
        }

        [HttpPost]
        public async Task<ActionResult<ViewInteractible>> PostInteractible(InputInteractible input)
        {
            var interactible = new DataInteractible
            {
                Name = input.Name,
                ImagePath = input.ImagePath
            };

            _context.Interactibles.Add(interactible);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = interactible.InteractibleID }, interactible);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var interactible = _context.Interactibles.Find(id);
            if (interactible == null)
            {
                return NotFound();
            }

            _context.Interactibles.Remove(interactible);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
