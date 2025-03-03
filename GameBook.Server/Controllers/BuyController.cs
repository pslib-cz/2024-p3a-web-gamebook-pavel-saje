using GameBook.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameBook.Server.Data;
using PowerArgs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BuyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewBuy>>> GetBuys(
            //[FromQuery] string access_token
            )
        {
            //if (string.IsNullOrEmpty(access_token))
            //{
            //    return Unauthorized("Token is missing.");
            //}

            //var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(access_token) as JwtSecurityToken;

            //if (jsonToken == null)
            //{
            //    return Unauthorized("Invalid token.");
            //}

            //// Pokud chceš použít role nebo jiné údaje z tokenu, můžeš to zde zkontrolovat.
            //var username = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

            //// Autorizace dle získaného username nebo jiného údaje z tokenu
            //if (username != "admin")
            //{
            //    return Unauthorized("Invalid user.");
            //}

            // Pokud je token validní, pokračuj ve zpracování
            var buys = await _context.Buy
                .Include(b => b.Interactible)
                .Include(b => b.Item)
                .Select(b => new ViewBuy
                {
                    BuyID = b.BuyID,
                    interactibleID = b.InteractibleID,
                    itemID = b.ItemID,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = b.Interactible.InteractibleID,
                        Name = b.Interactible.Name,
                        ImagePath = b.Interactible.ImagePath
                    },
                    Item = new ViewItem
                    {
                        ItemID = b.Item.ItemID,
                        Name = b.Item.Name,
                        TradeValue = b.Item.TradeValue,
                        Stackable = b.Item.Stackable,
                        CategoryId = b.Item.CategoryId,
                        RadiationGain = b.Item.RadiationGain,
                        Category = new ViewItemCategory
                        {
                            CategoryID = b.Item.Category.CategoryID,
                            Name = b.Item.Category.Name
                        }
                    }
                })
                .ToListAsync();

            return Ok(buys);
        }


        [HttpPost]
        public async Task<ActionResult<ViewBuy>> PostBuy(InputBuy inputBuy)
        {
            var dataBuy = new DataBuy
            {
                InteractibleID = inputBuy.InteractibleID,
                ItemID = inputBuy.ItemID
            };

            _context.Buy.Add(dataBuy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuys", new { id = dataBuy.BuyID }, inputBuy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuy(int id)
        {
            var dataBuy = await _context.Buy.FindAsync(id);
            if (dataBuy == null)
            {
                return NotFound();
            }

            _context.Buy.Remove(dataBuy);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
