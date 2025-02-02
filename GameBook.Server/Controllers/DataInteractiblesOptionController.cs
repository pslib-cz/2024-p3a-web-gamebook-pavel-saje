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
        public async Task<ActionResult<ViewInteractiblesOption>> GetInteractiblesOptions()
        {
            var Option = await _context.InteractiblesOption
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

        [HttpGet("Interactible/{id}")]
        public async Task<ActionResult<ViewInteractiblesOption>> rGetOptionsByInteractibleId(int id)
        {
            var Option = await _context.InteractiblesOption
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
    }
}
