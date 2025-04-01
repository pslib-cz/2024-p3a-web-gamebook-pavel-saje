using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndController: ControllerBase
    {
        private AppDbContext _context;
        public EndController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<ViewEnd> Get(int id)
        {
            var end = _context.Ends.Find(id);

            if (end == null)
            {
                return NotFound();
            }

            return Ok(new ViewEnd
            {
                EndID = end.EndID,
                LocationID = end.LocationID,
                DialogID = end.DialogID
            });
        }

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewEnd>> Post(InputEnd inputEnd)
        {
            var end = new DataEnd
            {
                LocationID = inputEnd.LocationID,
                DialogID = inputEnd.DialogID
            };

            _context.Ends.Add(end);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = end.EndID }, inputEnd);
        }

        [NonAction][HttpDelete("{id}")]
        public async Task<ActionResult<ViewEnd>> Delete(int id)
        {
            var end = await _context.Ends.FindAsync(id);

            if (end == null)
            {
                return NotFound();
            }

            _context.Ends.Remove(end);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
