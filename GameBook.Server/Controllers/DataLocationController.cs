using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataLocationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataLocationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataLocation>> Get()
        {
            return Ok(_context.Location.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataLocation> Get(int id)
        {
            var location = _context.Location
                .Include(l => l.LocationContents)
                .Include(l => l.RequiredItems)
                .FirstOrDefault(l => l.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpGet("GetNearestLocation/{locationId}")]
        public ActionResult<DataLocation> GetNearestLocation(int locationId)
        {
            List<DataLocation> NearBy = new List<DataLocation>();

            var locations = _context.LocationPath
                .Where(x => x.FirstNodeID == locationId || x.SecondNodeID == locationId)
                .ToList();

            var location = locations
                .Select(x => x.FirstNodeID == locationId ? x.SecondNodeID : x.FirstNodeID)
                .ToList();

            foreach (int loc in location)
            {
                var Near = _context.Location.Find(loc);
                NearBy.Add(Near);
            }

            return Ok(NearBy);
        }

        [HttpPost]
        public ActionResult<DataLocation> Post(DataLocation location)
        {
            _context.Location.Add(location);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = location.LocationID }, location);
        }

        [HttpPut("{id}")]
        public ActionResult<DataLocation> Put(int id, DataLocation location)
        {
            if (id != location.LocationID)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataLocation> Delete(int id)
        {
            var location = _context.Location.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
