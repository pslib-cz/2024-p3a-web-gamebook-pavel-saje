using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractOptionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InteractOptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataInteractOption>> Get()
        {
            return Ok(_context.InteractOptions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractOption> Get(int id)
        {
            var option = _context.InteractOptions
                .Include(o => o.Interactibles)
                .FirstOrDefault(o => o.OptionID == id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewInteractOption>> PostOption(InputInteractOption input)
        {
            var option = new DataInteractOption
            {
                OptionText = input.OptionText,
                
            };

            _context.InteractOptions.Add(option);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = option.OptionID }, option);
        }

        [NonAction][HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var option = _context.InteractOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractOptions.Remove(option);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
