using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataInteractibleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataInteractibleController(AppDbContext context)
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
                .Include(i => i.InteractOptions)
                .Include(i => i.LocationContents)
                .FirstOrDefault(i => i.InteractibleID == id);

            if (interactible == null)
            {
                return NotFound();
            }

            return Ok(interactible);
        }

        [HttpPost]
        public ActionResult<DataInteractible> Post(DataInteractible interactible)
        {
            _context.Interactibles.Add(interactible);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = interactible.InteractibleID }, interactible);
        }

        [HttpPut("{id}")]
        public ActionResult<DataInteractible> Put(int id, DataInteractible interactible)
        {
            if (id != interactible.InteractibleID)
            {
                return BadRequest();
            }

            _context.Entry(interactible).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataInteractible> Delete(int id)
        {
            var interactible = _context.Interactibles.Find(id);
            if (interactible == null)
            {
                return NotFound();
            }

            _context.Interactibles.Remove(interactible);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
