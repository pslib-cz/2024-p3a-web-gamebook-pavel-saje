using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpcController : ControllerBase
    {
        private AppDbContext _context;
        public NpcController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ByInteractibleId/{id}")]
        public ActionResult<ViewNpc> Get(int id)
        {
                var npc = _context.InteractiblesNpcs
                    .Include(inpc => inpc.Npc)
                    .ThenInclude(n => n.Weapon)
                    .FirstOrDefault(inpc => inpc.InteractibleID == id);

                if (npc == null)
                {
                    return NotFound();
                }

                return Ok(new ViewNpc
                {
                    NpcID = npc.Npc.NpcID,
                    Name = npc.Npc.Name,
                    Health = npc.Npc.Health,
                    Weapon = npc.Npc.Weapon == null ? null : new ViewWeapon
                    {
                        WeaponID = npc.Npc.Weapon.WeaponID,
                        Name = npc.Npc.Weapon.Name,
                        Damage = npc.Npc.Weapon.Damage,
                    }
                });
            }
        }
    }
