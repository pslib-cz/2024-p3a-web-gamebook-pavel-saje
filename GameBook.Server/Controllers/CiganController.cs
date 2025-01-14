using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;

namespace GameBook.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {

        private AppDbContext _context;
        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            return Ok(_context.Items.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetId(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("GetByInteractibleId/{id}")]
        public ActionResult<Item> GetByInteractibleId(int id)
        {
            var InteractibleItem = _context.InteractiblesItems
                .Where(x => x.InteractibleID == id)
                .ToArray();

            var item = _context.Items.Find(InteractibleItem[0].ItemId);

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> Post(Item item)
        {
            var category = _context.ItemCategories.Find(item.CategoryId);

            if(category == null)
            {
                return BadRequest($"Category with ID {item.CategoryId} does not exist.");
            }

            item.Category = category;

            _context.Items.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemCategory>> Get()
        {
            return Ok(_context.ItemCategories.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemCategory> GetId(int id)
        {
            var itemCategory = _context.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return Ok(itemCategory);
        }


        [HttpPost]
        public ActionResult<ItemCategory> Post(ItemCategory itemCategory)
        {
            _context.ItemCategories.Add(itemCategory);
            _context.SaveChanges();
            return Ok(itemCategory);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var itemCategory = _context.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            _context.ItemCategories.Remove(itemCategory);
            _context.SaveChanges();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ConsumableController : Controller
    {
        private AppDbContext _context;

        public ConsumableController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ConsumableItem>> Get()
        {
            return Ok(_context.ConsumableItems.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ConsumableItem> GetId(int id)
        {
            var consumableItem = _context.ConsumableItems.Find(id);
            if (consumableItem == null)
            {
                return NotFound();
            }
            return Ok(consumableItem);
        }


        [HttpPost]
        public ActionResult<ConsumableItem> Post(ConsumableItem consumableItem)
        {
            var Item = _context.Items.Find(consumableItem.ItemID);

            if (Item == null)
            {
                return BadRequest($"Item with ID {consumableItem.ItemID} does not exist.");
            }

            consumableItem.Item = Item;
            _context.ConsumableItems.Add(consumableItem);
            _context.SaveChanges();

            return Ok(consumableItem);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var consumableItem = _context.ConsumableItems.Find(id);
            if (consumableItem == null)
            {
                return NotFound();
            }

            _context.ConsumableItems.Remove(consumableItem);
            _context.SaveChanges();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class EndController : Controller
    {
        private AppDbContext _context;

        public EndController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<End>> Get()
        {
            return Ok(_context.End.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<End> GetId(int id)
        {
            var end = _context.End.Find(id);
            if (end == null)
            {
                return NotFound();
            }
            return Ok(end);
        }
    }
    }
