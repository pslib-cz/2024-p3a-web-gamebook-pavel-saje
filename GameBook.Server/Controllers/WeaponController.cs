using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private AppDbContext _context;
        public WeaponController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Item/{id}")]
        public ActionResult<ViewWeapon> Get(int id)
        {
            var weapon = _context.Weapons
                                .Include(w => w.Item)
                .FirstOrDefault(w => w.ItemID == id);

            if (weapon == null)
            {
                return NotFound();
            }

            return Ok(new ViewWeapon
            {
                WeaponID = weapon.WeaponID,
                Name = weapon.Name,
                Damage = weapon.Damage,
                Item = new ViewItem
                {
                    ItemID = weapon.Item.ItemID,
                    Name = weapon.Item.Name,
                    TradeValue = weapon.Item.TradeValue,
                    Stackable = weapon.Item.Stackable,
                    Category = new ViewItemCategory
                    {
                        CategoryID = weapon.Item.CategoryId,
                        Name = weapon.Item.Name,
                    }
                }
            });
        }
    }
}
