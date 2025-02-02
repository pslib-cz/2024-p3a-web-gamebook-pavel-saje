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
                    InteractibleID = dialog.IteractibleID
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
        public ActionResult<DataDialog> Post(DataDialog dialog)
        {
            _context.Dialogs.Add(dialog);
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
            var dialog = _context.Dialogs.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }

            _context.Dialogs.Remove(dialog);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
