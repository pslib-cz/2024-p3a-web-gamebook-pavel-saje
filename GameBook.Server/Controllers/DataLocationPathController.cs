using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataLocationPathController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataLocationPathController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataLocationPath>> Get()
        {
            return Ok(_context.LocationPath.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataLocationPath> Get(int id)
        {
            var path = _context.LocationPath
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
        public ActionResult<DataLocationPath> Post(DataLocationPath path)
        {
            _context.LocationPath.Add(path);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = path.PathID }, path);
        }

        [HttpPut("{id}")]
        public ActionResult<DataLocationPath> Put(int id, DataLocationPath path)
        {
            if (id != path.PathID)
            {
                return BadRequest();
            }

            _context.Entry(path).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataLocationPath> Delete(int id)
        {
            var path = _context.LocationPath.Find(id);
            if (path == null)
            {
                return NotFound();
            }

            _context.LocationPath.Remove(path);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
