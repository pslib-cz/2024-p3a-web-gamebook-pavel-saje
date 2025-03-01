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

        [HttpGet("ByContentId/{id}")]
        public ActionResult<ViewNpc> Get(int id)
        {
            var content = _context.LocationContents
                .Include(l => l.Location)
                .Include(l => l.Interactible)
                .Select(l => new DataLocationContent
                {
                    LocationContentID = l.LocationContentID,
                    LocationID = l.LocationID,
                    InteractibleID = l.InteractibleID,
                    XPos = l.XPos,
                    YPos = l.YPos,
                    Location = new DataLocation
                    {
                        LocationID = l.Location.LocationID,
                        Name = l.Location.Name,
                        BackgroundImagePath = l.Location.BackgroundImagePath,
                        RadiationGain = l.Location.RadiationGain,
                    },
                    Interactible = new DataInteractible
                    {
                        InteractibleID = l.Interactible.InteractibleID,
                        Name = l.Interactible.Name,
                        ImagePath = l.Interactible.ImagePath,
                    }
                })
                .FirstOrDefault(l => l.LocationContentID == id);

            var npc = _context.InteractiblesNpcs
                    .Include(inpc => inpc.Npc)
                    .ThenInclude(n => n.Weapon)
                    .FirstOrDefault(inpc => inpc.InteractibleID == content.InteractibleID);

                if (npc == null)
                {
                    return NotFound();
                }

            var NpcContent = new NPCnContent
            {
                Npc = new ViewNpc
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
                },
                Content = content
            };

                return Ok(NpcContent);
        }

        [HttpPost]
        public async Task<ActionResult<ViewNpc>> Post(InputNpc input)
        {
            var npc = new DataNpc
            {
                Name = input.Name,
                Health = input.Health,
                WeaponID = input.WeaponID
            };

            _context.Npcs.Add(npc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = npc.NpcID }, npc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var npc = _context.Npcs.Find(id);
            if (npc == null)
            {
                return NotFound();
            }

            _context.Npcs.Remove(npc);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class NPCnContent
    {
        public ViewNpc Npc { get; set; }
        public DataLocationContent Content { get; set; }
    }
    }
