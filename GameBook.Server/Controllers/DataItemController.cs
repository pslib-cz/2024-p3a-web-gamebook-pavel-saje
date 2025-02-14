using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBook.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameBook.Server.Data;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

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

        [HttpGet("Consumables&Weapons")]
        public async Task<ActionResult> GetItemTypes()
        {
            var Consumables = _context.ConsumableItems
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
            var Weapons = _context.Weapons
                                .Include(w => w.Item)
                                .Select(w => new ViewWeapon
                                {
                                    WeaponID = w.WeaponID,
                                    Name = w.Name,
                                    Damage = w.Damage,
                                    Item = new ViewItem
                                    {
                                        ItemID = w.Item.ItemID,
                                        Name = w.Item.Name,
                                        TradeValue = w.Item.TradeValue,
                                        Stackable = w.Item.Stackable,
                                        Category = new ViewItemCategory
                                        {
                                            CategoryID = w.Item.CategoryId,
                                            Name = w.Item.Name,
                                        }
                                    }
                                }).ToList();

            var itemTypes = new itemTypes
            {
                Weapons = Weapons,
                Consumables = Consumables
            };

            return Ok(itemTypes);
        }
    }

    internal class itemTypes
    {
        public List<ViewWeapon> Weapons { get; set; }
        public List<ViewConsumableItem> Consumables { get; set; }
    }
}
