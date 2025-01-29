using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableItemController : ControllerBase
    {
        private AppDbContext _context;
        public ConsumableItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ViewConsumableItem>> Get()
        {
            var consumableItems = _context.ConsumableItem
                .Include(ci => ci.Item)
                .Select(ci => new ViewConsumableItem
                {
                    ConsumableItemID = ci.ConsumableItemID,
                    ItemID = ci.ItemID,
                    HealthValue = ci.HealthValue,
                    EnergyValue = ci.EnergyValue,
                    RadiationValue = ci.RadiationValue,
                    Item = new ViewItem
                    {
                        ItemID = ci.Item.ItemID,
                        Name = ci.Item.Name,
                        TradeValue = ci.Item.TradeValue,
                        Stackable = ci.Item.Stackable,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ci.Item.CategoryId,
                            Name = ci.Item.Name,
                        }
                    }
                })
                .ToList();

            return Ok(consumableItems);

        }

        [HttpGet("{id}")]
        public ActionResult<ViewConsumableItem> Get(int id)
        {
            var consumableItem = _context.ConsumableItem
                .Include(ci => ci.Item)
                .ThenInclude(i => i.Category)
                .FirstOrDefault(ci => ci.ItemID == id);

            if (consumableItem == null)
            {
                return NotFound();
            }

            return Ok(new ViewConsumableItem
                {
                    ConsumableItemID = consumableItem.ConsumableItemID,
                    ItemID = consumableItem.ItemID,
                    HealthValue = consumableItem.HealthValue,
                    EnergyValue = consumableItem.EnergyValue,
                    RadiationValue = consumableItem.RadiationValue,
                    Item = new ViewItem
                    {
                        ItemID = consumableItem.Item.ItemID,
                        Name = consumableItem.Item.Name,
                        TradeValue = consumableItem.Item.TradeValue,
                        Stackable = consumableItem.Item.Stackable,
                        Category = new ViewItemCategory
                        {
                            CategoryID = consumableItem.Item.CategoryId,
                            Name = consumableItem.Item.Name,
                        }
                    }
                }
            );
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
