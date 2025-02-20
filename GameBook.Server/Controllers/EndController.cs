using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndController: ControllerBase
    {
        private AppDbContext _context;
        public EndController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<ViewEnd> Get(int id)
        {
            var end = _context.Ends.Find(id);

            if (end == null)
            {
                return NotFound();
            }

            return Ok(new ViewEnd
            {
                EndID = end.EndID,
                LocationID = end.LocationID,
                DialogID = end.DialogID
            });
        }
    }
}
