using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataInteractOptionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataInteractOptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataInteractOption>> Get()
        {
            return Ok(_context.InteractOptions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataInteractOption> Get(int id)
        {
            var option = _context.InteractOptions
                .Include(o => o.Interactibles)
                .FirstOrDefault(o => o.OptionID == id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }

        [HttpPost]
        public ActionResult<DataInteractOption> Post(DataInteractOption option)
        {
            _context.InteractOptions.Add(option);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = option.OptionID }, option);
        }

        [HttpPut("{id}")]
        public ActionResult<DataInteractOption> Put(int id, DataInteractOption option)
        {
            if (id != option.OptionID)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataInteractOption> Delete(int id)
        {
            var option = _context.InteractiblesOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractiblesOptions.Remove(option);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
