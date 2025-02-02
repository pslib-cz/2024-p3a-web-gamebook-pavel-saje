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
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewItem>> GetItemById(int id)
        {
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

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
