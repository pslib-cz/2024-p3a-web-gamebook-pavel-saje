using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataItemController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataItem>> Get()
        {
            return Ok(_context.Item.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataItem> Get(int id)
        {
            var item = _context.Item
                .Include(i => i.Category)
                .Include(i => i.RequiredInLocations)
                .FirstOrDefault(i => i.ItemID == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<DataItem> Post(DataItem item)
        {
            _context.Item.Add(item);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = item.ItemID }, item);
        }

        [HttpPut("{id}")]
        public ActionResult<DataItem> Put(int id, DataItem item)
        {
            if (id != item.ItemID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataItem> Delete(int id)
        {
            var item = _context.Item.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
