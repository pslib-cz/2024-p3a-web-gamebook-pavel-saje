using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataConsumableItemController : ControllerBase
    {
        private AppDbContext _context;
        public DataConsumableItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataConsumableItem>> Get()
        {
            return Ok(_context.ConsumableItem.ToList());

        }

        [HttpGet("{id}")]
        public ActionResult<DataConsumableItem> Get(int id)
        {
            var consumableItem = _context.ConsumableItem.Find(id);
            if (consumableItem == null)
            {
                return NotFound();
            }
            return Ok(consumableItem);
        }

        [HttpPost]
        public ActionResult<DataConsumableItem> Post(DataConsumableItem consumableItem)
        {
            _context.ConsumableItem.Add(consumableItem);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = consumableItem.ConsumableItemID }, consumableItem);
        }

        [HttpPut("{id}")]
        public ActionResult<DataConsumableItem> Put(int id, DataConsumableItem consumableItem)
        {
            if (id != consumableItem.ConsumableItemID)
            {
                return BadRequest();
            }
            _context.Entry(consumableItem).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataConsumableItem> Delete(int id)
        {
            var consumableItem = _context.ConsumableItem.Find(id);
            if (consumableItem == null)
            {
                return NotFound();
            }
            _context.ConsumableItem.Remove(consumableItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
