using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders.Composite;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationContentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LocationContentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataLocationContent>> Get()
        {
            var contents = _context.LocationContents
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
                }).ToList();
            return Ok(_context.LocationContents.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataLocationContent> Get(int id)
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

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<ViewLocationContent>> Post(InputLocationContent input)
        {
            var content = new DataLocationContent
            {
                LocationID = input.LocationID,
                InteractibleID = input.InteractibleID,
                XPos = input.XPos,
                YPos = input.YPos,
                size = input.size
            };

            _context.LocationContents.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = content.LocationContentID }, content);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var content = _context.LocationContents.Find(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.LocationContents.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
