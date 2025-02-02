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
        public async Task<ActionResult<IEnumerable<ViewInteractiblesItem>>> GetInteractiblesItems()
        {
<<<<<<< HEAD
            var interactibleItems = await _context.InteractiblesItem
                .Include(i => i.Item)
                .Select(i => new ViewInteractiblesItem
                {
                    InteractibleID = i.InteractibleID,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = i.Interactible.InteractibleID,
                        Name = i.Interactible.Name,
                        ImagePath = i.Interactible.ImagePath
                    },
                    ItemId = i.ItemId,
                    Item = new ViewItem
                    {
                        ItemID = i.Item.ItemID,
                        Name = i.Item.Name,
                        TradeValue = i.Item.TradeValue,
                        Stackable = i.Item.Stackable,
                        CategoryId = i.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = i.Item.Category.CategoryID,
                            Name = i.Item.Category.Name
                        }
                    }
                    
                }).ToListAsync();

            return Ok(interactibleItems);
=======
            return Ok(_context.InteractiblesItems.ToList());
>>>>>>> 3ae8db0ae371b3abcf923d6a0a3f6d7406feca78
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewItem>> GetItemById(int id)
        {
<<<<<<< HEAD
            var item = await _context.Item
                .Include(i => i.Category)
                .Where(i => i.ItemID == id)
                .Select(i => new ViewItem
                {
                    ItemID = i.ItemID,
                    Name = i.Name,
                    TradeValue = i.TradeValue,
                    Stackable = i.Stackable,
                    CategoryId = i.CategoryId,
                    Category = new ViewItemCategory
                    {
                        CategoryID = i.Category.CategoryID,
                        Name = i.Category.Name
                    }
                }).FirstOrDefaultAsync();
=======
            var item = _context.InteractiblesItems
                .Include(i => i.Interactible)
                .Include(i => i.Item)
                .FirstOrDefault(i => i.InteractiblesItemID == id);
>>>>>>> 3ae8db0ae371b3abcf923d6a0a3f6d7406feca78

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
<<<<<<< HEAD
=======

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
>>>>>>> 3ae8db0ae371b3abcf923d6a0a3f6d7406feca78
    }
}
