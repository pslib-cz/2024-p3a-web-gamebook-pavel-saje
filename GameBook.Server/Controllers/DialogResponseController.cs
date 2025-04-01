using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogResponseController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DialogResponseController(AppDbContext context)
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

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewDialogResponse>> PostResponse(InputDialogResponse inputResponse)
        {
            var dialog = await _context.Dialogs.FindAsync(inputResponse.DialogID);
            if (dialog == null)
            {
                return BadRequest("Invalid DialogID.");
            }

            var dataResponse = new DataDialogResponse
            {
                DialogID = inputResponse.DialogID,
                NextDialogID = inputResponse.NextDialogID,
                ResponseText = inputResponse.ResponseText,
                RelationshipEffect = inputResponse.RelationshipEffect,
                Dialog = dialog
            };

            _context.DialogResponses.Add(dataResponse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = dataResponse.DialogResponseID }, inputResponse);
        }

        [NonAction][HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = _context.DialogResponses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            _context.DialogResponses.Remove(response);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
