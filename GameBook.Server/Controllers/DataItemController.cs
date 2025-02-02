using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBook.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameBook.Server.Data;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewItem>>> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Category)
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
                }).ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewItem>> GetItemById(int id)
        {
            var item = await _context.Items
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
