using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDialogResponseController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataDialogResponseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataDialogResponse>> Get()
        {
            return Ok(_context.DialogResponses.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataDialogResponse> Get(int id)
        {
            var response = _context.DialogResponses
                .Include(d => d.Dialog)
                .Include(d => d.NextDialog)
                .FirstOrDefault(d => d.DialogResponseID == id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<DataDialogResponse> Post(DataDialogResponse response)
        {
            _context.DialogResponses.Add(response);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = response.DialogResponseID }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<DataDialogResponse> Put(int id, DataDialogResponse response)
        {
            if (id != response.DialogResponseID)
            {
                return BadRequest();
            }

            _context.Entry(response).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataDialogResponse> Delete(int id)
        {
            var response = _context.DialogResponses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            _context.DialogResponses.Remove(response);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
