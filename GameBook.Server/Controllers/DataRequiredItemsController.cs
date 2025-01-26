using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRequiredItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataRequiredItemsController(AppDbContext context)
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

        [HttpPost]
        public ActionResult<DataRequiredItems> Post(DataRequiredItems requiredItem)
        {
            _context.RequiredItems.Add(requiredItem);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = requiredItem.RequiredItemsID }, requiredItem);
        }

        [HttpPut("{id}")]
        public ActionResult<DataRequiredItems> Put(int id, DataRequiredItems requiredItem)
        {
            if (id != requiredItem.RequiredItemsID)
            {
                return BadRequest();
            }

            _context.Entry(requiredItem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataRequiredItems> Delete(int id)
        {
            var requiredItem = _context.RequiredItems.Find(id);
            if (requiredItem == null)
            {
                return NotFound();
            }

            _context.RequiredItems.Remove(requiredItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
