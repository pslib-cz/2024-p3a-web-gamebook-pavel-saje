using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataInteractiblesOptionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataInteractiblesOptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataInteractiblesOption>> Get()
        {
            var options = _context.InteractiblesOptions
                .Include(o => o.Interactible)
                .Include(o => o.Option)
                .Select(o => new DataInteractiblesOption
                {
                    InteractiblesOptionID = o.InteractiblesOptionID,
                    InteractibleID = o.InteractibleID,
                    OptionID = o.OptionID,
                    Interactible = new DataInteractible
                    {
                        InteractibleID = o.Interactible.InteractibleID,
                        ImagePath = o.Interactible.ImagePath,
                        Name = o.Interactible.Name,
                    },
                    Option = new DataInteractOption
                    {
                        OptionID = o.Option.OptionID,
                        OptionText = o.Option.OptionText
                    }
                }).ToList();

            return Ok(options);
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractiblesOption> Get(int id)
        {
            var option = _context.InteractiblesOptions
                .Include(o => o.Interactible)
                .Include(o => o.Option)
                .FirstOrDefault(o => o.InteractiblesOptionID == id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }

        [HttpGet("Interactible/{id}")]
        public async Task<ActionResult<ViewInteractiblesOption>> rGetOptionsByInteractibleId(int id)
        {
            var Option = await _context.InteractiblesOptions
                .Where(i => i.InteractibleID == id)
                .Include(i => i.Option)
                .Select(i => new ViewInteractiblesOption
                {
                    InteractibleID = i.InteractibleID,
                    OptionID = i.OptionID,
                    Option = new ViewInteractOption
                    {
                        OptionID = i.Option.OptionID,
                        OptionText = i.Option.OptionText
                    }
                }).ToListAsync();


            if (Option == null)
            {
                return NotFound();
            }

            return Ok(Option);
        }

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewInteractiblesOption>> PostInteractiblesOption(InputInteractiblesOption inputInteractiblesOption)
        {
            var option = new DataInteractiblesOption
            {
                InteractibleID = inputInteractiblesOption.InteractibleID,
                OptionID = inputInteractiblesOption.OptionID
            };

            _context.InteractiblesOptions.Add(option);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = option.InteractiblesOptionID }, option);
        }

        [NonAction][HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var option = _context.InteractiblesOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractiblesOptions.Remove(option);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
