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
            return Ok(_context.InteractiblesOption.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractiblesOption> Get(int id)
        {
            var option = _context.InteractiblesOption
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

        [HttpPost]
        public ActionResult<DataInteractiblesOption> Post(DataInteractiblesOption option)
        {
            _context.InteractiblesOption.Add(option);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = option.InteractiblesOptionID }, option);
        }

        [HttpPut("{id}")]
        public ActionResult<DataInteractiblesOption> Put(int id, DataInteractiblesOption option)
        {
            if (id != option.InteractiblesOptionID)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataInteractiblesOption> Delete(int id)
        {
            var option = _context.InteractiblesOption.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractiblesOption.Remove(option);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
