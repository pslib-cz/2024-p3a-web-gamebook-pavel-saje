using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequiredItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RequiredItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataRequiredItems>> Get()
        {
            return Ok(_context.RequiredItems.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataRequiredItems> Get(int id)
        {
            var requiredItem = _context.RequiredItems
                .Include(ri => ri.Location)
                .Include(ri => ri.Item)
                .FirstOrDefault(ri => ri.RequiredItemsID == id);

            if (requiredItem == null)
            {
                return NotFound();
            }

            return Ok(requiredItem);
        }

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewRequiredItems>> Post(InputRequiredItem requiredItem)
        {
            var dataRequiredItem = new DataRequiredItems
            {
                LocationID = requiredItem.LocationID,
                ItemID = requiredItem.ItemID
            };

            _context.RequiredItems.Add(dataRequiredItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dataRequiredItem.RequiredItemsID }, requiredItem);
        }

        [NonAction][HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var requiredItem = _context.RequiredItems.Find(id);
            if (requiredItem == null)
            {
                return NotFound();
            }

            _context.RequiredItems.Remove(requiredItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
