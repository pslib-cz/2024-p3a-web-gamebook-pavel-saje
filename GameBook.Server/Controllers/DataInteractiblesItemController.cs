using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataInteractiblesItemController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataInteractiblesItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataInteractiblesItem>> Get()
        {
            var items = _context.InteractiblesItems
                .Include(i => i.Interactible)
                .Include(i => i.Item)
                .Select(i => new DataInteractiblesItem
                {
                    InteractiblesItemID = i.InteractiblesItemID,
                    InteractibleID = i.InteractibleID,
                    ItemId = i.ItemId,
                    Interactible = new DataInteractible
                    {
                        InteractibleID = i.Interactible.InteractibleID,
                        ImagePath = i.Interactible.ImagePath,
                        Name = i.Interactible.Name,
                    },
                    Item = new DataItem
                    {
                        ItemID = i.Item.ItemID,
                        Name = i.Item.Name,
                        Category = i.Item.Category,
                        Stackable = i.Item.Stackable,
                        TradeValue = i.Item.TradeValue,
                        RadiationGain = i.Item.RadiationGain,
                    }
                }).ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractiblesItem> Get(int id)
        {
            var item = _context.InteractiblesItems
                .Include(i => i.Interactible)
                .Include(i => i.Item)
                .FirstOrDefault(i => i.InteractiblesItemID == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<DataInteractiblesItem> Post(DataInteractiblesItem item)
        {
            _context.InteractiblesItems.Add(item);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = item.InteractiblesItemID }, item);
        }

        [HttpPut("{id}")]
        public ActionResult<DataInteractiblesItem> Put(int id, DataInteractiblesItem item)
        {
            if (id != item.InteractiblesItemID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataInteractiblesItem> Delete(int id)
        {
            var item = _context.InteractiblesItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.InteractiblesItems.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}