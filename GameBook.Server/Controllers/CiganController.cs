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



        [HttpPost]
        public ActionResult<Item> Post(Item item)
        {
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
}
