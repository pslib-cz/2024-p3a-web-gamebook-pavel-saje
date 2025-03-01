using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationPathController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LocationPathController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataLocationPath>> Get()
        {
            return Ok(_context.LocationPaths.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataLocationPath> Get(int id)
        {
            var path = _context.LocationPaths
                .Include(lp => lp.FirstNode)
                .Include(lp => lp.SecondNode)
                .FirstOrDefault(lp => lp.PathID == id);

            if (path == null)
            {
                return NotFound();
            }

            return Ok(path);
        }

        [HttpPost]
        public async Task<ActionResult<DataLocationPath>> Post(InputLocationPath input)
        {
            var path = new DataLocationPath
            {
                FirstNodeID = input.FirstNodeID,
                SecondNodeID = input.SecondNodeID,
                EnergyTravelCost = input.EnergyTravelCost
            };

            _context.LocationPaths.Add(path);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = path.PathID }, path);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var path = _context.LocationPaths.Find(id);
            if (path == null)
            {
                return NotFound();
            }

            _context.LocationPaths.Remove(path);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
