using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DialogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ViewDialog>> Get()
        {
            var dialogs = _context.Dialogs
                .Include(d => d.DialogResponses)
                .ThenInclude(dr => dr.NextDialog)
                .Select(d => new ViewDialog
                {
                    DialogID = d.DialogID,
                    NextDialogID = d.NextDialogID,
                    FromInteractibleID = d.FromInteractibleID,
                    Text = d.Text,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = d.IteractibleID,
                        ImagePath = d.Interactible.ImagePath,
                        Name = d.Interactible.Name,
                    },
                    FromInteractible = d.FromInteractible == null ? null : new ViewInteractible
                    {
                        InteractibleID = d.FromInteractible.InteractibleID,
                        ImagePath = d.FromInteractible.ImagePath,
                        Name = d.FromInteractible.Name,
                    },
                    DialogResponses = d.DialogResponses.Select(dr => new ViewDialogResponse
                    {
                        DialogResponseID = dr.DialogResponseID,
                        DialogID = dr.DialogID,
                        NextDialogID = dr.NextDialogID,
                        ResponseText = dr.ResponseText,
                        RelationshipEffect = dr.RelationshipEffect,
                    }).ToList()
                })
                .ToList();

            return Ok(dialogs);
        }

        [HttpGet("{id}")]
        public ActionResult<ViewDialog> Get(int id)
        {
            var dialog = _context.Dialogs
                .Include(d => d.DialogResponses)
                .ThenInclude(dr => dr.NextDialog)
                 .Include(d => d.Interactible)
                .FirstOrDefault(d => d.DialogID == id);

            if (dialog == null)
            {
                return NotFound();
            }

            return Ok(new ViewDialog
            {
                DialogID = dialog.DialogID,
                NextDialogID = dialog.NextDialogID,
                Text = dialog.Text,
                Interactible = new ViewInteractible
                {
                    InteractibleID = dialog.IteractibleID,
                    Name = dialog.Interactible.Name,
                    ImagePath = dialog.Interactible.ImagePath,

                },
                DialogResponses = dialog.DialogResponses.Select(dr => new ViewDialogResponse
                {
                    DialogResponseID = dr.DialogResponseID,
                    DialogID = dr.DialogID,
                    NextDialogID = dr.NextDialogID,
                    ResponseText = dr.ResponseText,
                    RelationshipEffect = dr.RelationshipEffect,
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<ViewDialog>> PostDialog(InputDialog inputDialog)
        {
            var dataDialog = new DataDialog
            {
                FromInteractibleID = inputDialog.FromInteractibleID,
                IteractibleID = inputDialog.IteractibleID,
                NextDialogID = inputDialog.NextDialogID,
                Text = inputDialog.Text
            };

            _context.Dialogs.Add(dataDialog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDialog", new { id = dataDialog.DialogID }, inputDialog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDialog(int id)
        {
            var dataDialog = _context.Dialogs.Find(id);
            if (dataDialog == null)
            {
                return NotFound();
            }

            _context.Dialogs.Remove(dataDialog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
