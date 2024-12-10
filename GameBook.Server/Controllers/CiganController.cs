using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;


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
        public ActionResult<IEnumerable<Items>> Get()
        {
            return Ok(_context.Items.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Items> GetId(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }



        [HttpPost]
        public ActionResult<Items> Post(Items item)
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
        public ActionResult<IEnumerable<ItemCategories>> Get()
        {
            return Ok(_context.ItemCategories.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemCategories> GetId(int id)
        {
            var itemCategory = _context.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return Ok(itemCategory);
        }


        [HttpPost]
        public ActionResult<ItemCategories> Post(ItemCategories itemCategory)
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
        public ActionResult<IEnumerable<ConsumableItems>> Get()
        {
            return Ok(_context.ConsumableItems.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ConsumableItems> GetId(int id)
        {
            var consumableItem = _context.ConsumableItems.Find(id);
            if (consumableItem == null)
            {
                return NotFound();
            }
            return Ok(consumableItem);
        }


        [HttpPost]
        public ActionResult<ConsumableItems> Post(ConsumableItems consumableItem)
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
