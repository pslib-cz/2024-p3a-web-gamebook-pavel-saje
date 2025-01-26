using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDialogController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataDialogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataDialog>> Get()
        {
            return Ok(_context.DataDialogs.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataDialog> Get(int id)
        {
            var dialog = _context.DataDialogs
                .Include(d => d.DialogResponses)
                .Include(d => d.Interactible)
                .Include(d => d.NextDialog)
                .FirstOrDefault(d => d.DialogID == id);

            if (dialog == null)
            {
                return NotFound();
            }

            return Ok(dialog);
        }

        [HttpPost]
        public ActionResult<DataDialog> Post(DataDialog dialog)
        {
            _context.DataDialogs.Add(dialog);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = dialog.DialogID }, dialog);
        }

        [HttpPut("{id}")]
        public ActionResult<DataDialog> Put(int id, DataDialog dialog)
        {
            if (id != dialog.DialogID)
            {
                return BadRequest();
            }

            _context.Entry(dialog).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataDialog> Delete(int id)
        {
            var dialog = _context.DataDialogs.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }

            _context.DataDialogs.Remove(dialog);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
