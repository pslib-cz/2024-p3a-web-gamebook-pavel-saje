using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;


namespace GameBook.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractiblesController : Controller
    {
        private AppDbContext _context;
        public InteractiblesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Interactible>> Get()
        {
            return Ok(_context.Interactibles.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Interactible> GetId(int id)
        {
            var interactible = _context.Interactibles.Find(id);
            if (interactible == null)
            {
                return NotFound();
            }
            return Ok(interactible);
        }

        [HttpPost]
        public ActionResult<Interactible> Post([FromForm] Interactible interactible)
        {
            if (interactible.Image == null || interactible.Image.Length == 0)
            {
                return BadRequest("BackgroundImage not provided.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Interactibles");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // rename file
            string fileExtension = Path.GetExtension(interactible.Image.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(interactible.Image.FileName);
            uniqueFileName = string.Join("_", uniqueFileName.Split(Path.GetInvalidFileNameChars())); // Sanitize file name

            interactible.ImagePath = Path.Combine("/Uploads/Interactibles", uniqueFileName).Replace("\\", "/");

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    interactible.Image.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            _context.Interactibles.Add(interactible);
            _context.SaveChanges();
            return Ok(interactible);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var interactible = _context.Interactibles.Find(id);
            if (interactible == null)
            {
                return NotFound();
            }

            if (interactible.ImagePath != null)
            {
                //temporary solution - Path.Combile() requires directories to be separated
                char separator = Path.AltDirectorySeparatorChar;
                string fileLocation = Directory.GetCurrentDirectory() + separator + "wwwroot" + separator + interactible.ImagePath;

                if (System.IO.File.Exists(fileLocation))
                {
                    try
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error deleting file: {ex.Message}");
                    }
                }
                else
                {
                    return NotFound($"File {fileLocation} not found.");
                }
            }

            _context.Interactibles.Remove(interactible);
            _context.SaveChanges();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class InteractibleOptionsController : Controller
    {
        private AppDbContext _context;
        public InteractibleOptionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InteractiblesOption>> Get()
        {
            return Ok(_context.InteractiblesOptions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<InteractiblesOption> GetId(int id)
        {
            var option = _context.InteractiblesOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        [HttpPost]
        public ActionResult<InteractiblesOption> Post(InteractiblesOption interactiblesOption)
        {
            var interactible = _context.Interactibles.Find(interactiblesOption.InteractibleID);
            var option = _context.InteractOptions.Find(interactiblesOption.OptionID);

            if (interactible == null)
            {
                return BadRequest($"Interactible with ID {interactiblesOption.InteractibleID} does not exist.");
            }
            if (option == null)
            {
                return BadRequest($"Option with ID {interactiblesOption.OptionID} does not exist.");
            }

            interactiblesOption.Interactible = interactible;
            interactiblesOption.Option = option;

            _context.InteractiblesOptions.Add(interactiblesOption);
            _context.SaveChanges();
            return Ok(interactiblesOption);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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

    [ApiController]
    [Route("api/[controller]")]
    public class InteractibleItemsController : Controller
    {

        private AppDbContext _context;
        public InteractibleItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InteractiblesItem>> Get()
        {
            return Ok(_context.InteractiblesItems.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<InteractiblesItem> GetId(int id)
        {
            var interactibleItem = _context.InteractiblesItems.Find(id);
            if (interactibleItem == null)
            {
                return NotFound();
            }
            return Ok(interactibleItem);
        }

        [HttpGet("GetInteractibleIDs/{locationId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetInteractibleIDs(int locationId)
        {
            var interactibleIds = await _context.LocationContent
                .Where(x => x.LocationID == locationId)
                .Select(x => x.InteractibleID)
                .ToListAsync();

            var interactibleItems = await _context.InteractiblesItems
                .Where(item => interactibleIds.Contains(item.InteractibleID))
                .Select(x => x.ItemId)
                .ToListAsync();

            var items = await _context.Items
                .Where(item => interactibleItems.Contains(item.ItemID))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("api/interactibleItemByInteractibleId/{interactibleId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetInteractibleItems(int interactibleId)
        {
            var interactibleItems = await _context.InteractiblesItems
                .Where(item => item.InteractibleID == interactibleId)
                .Select(x => x.ItemId)
                .ToListAsync();

            var items = await _context.Items
                .Where(item => interactibleItems.Contains(item.ItemID))
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public ActionResult<InteractiblesItem> Post(InteractiblesItem interactibleItem)
        {
            var interactible = _context.Interactibles.Find(interactibleItem.InteractibleID);
            var item = _context.Items.Find(interactibleItem.ItemId);

            if (interactible == null)
            {
                return BadRequest($"Interactible with ID {interactibleItem.InteractibleID} does not exist.");
            }

            if (item == null)
            {
                return BadRequest($"Item with ID {interactibleItem.ItemId} does not exist.");
            }

            interactibleItem.Interactible = interactible;
            interactibleItem.Item = item;

            _context.InteractiblesItems.Add(interactibleItem);
            _context.SaveChanges();
            return Ok(interactibleItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InteractiblesItem>> Delete(int id)
        {
            var item = _context.InteractiblesItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.InteractiblesItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        } 

    }

    [ApiController]
    [Route("api/[controller]")]
    public class OptionsEnumController : Controller
    {
        private AppDbContext _context;
        public OptionsEnumController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InteractOption>> Get()
        {
            return Ok(_context.InteractOptions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<InteractOption> GetId(int id)
        {
            var option = _context.InteractOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        [HttpPost]
        public ActionResult<InteractOption> Post(InteractOption option)
        {
            _context.InteractOptions.Add(option);
            _context.SaveChanges();
            return Ok(option);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var option = _context.InteractOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractOptions.Remove(option);
            _context.SaveChanges();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NpcDialogController : Controller
    {
        private AppDbContext _context;
        public NpcDialogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dialog>> Get()
        {
            return Ok(_context.Dialogs.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Dialog> GetId(int id)
        {
            var dialog = _context.Dialogs.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }
            return Ok(dialog);
        }

        [HttpPost]
        public ActionResult<Dialog> Post(Dialog dialog)
        {
            var interactible = _context.Interactibles.Find(dialog.IteractibleID);

            if (interactible == null)
            {
                return BadRequest($"Interactible with ID {dialog.IteractibleID} does not exist.");
            }

            dialog.Interactible = interactible;

            _context.Dialogs.Add(dialog);
            _context.SaveChanges();
            return Ok(dialog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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

    [ApiController]
    [Route("api/[controller]")]
    public class NpcDialogResponsesController : Controller
    {
        private AppDbContext _context;
        public NpcDialogResponsesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DialogResponse>> Get()
        {
            return Ok(_context.DialogResponses.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DialogResponse> GetId(int id)
        {
            var response = _context.DialogResponses.Find(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<DialogResponse> Post(DialogResponse response)
        {
            var dialog = _context.Dialogs.Find(response.DialogID);
            
            if (dialog == null)
            {
                return BadRequest($"Dialog with ID {response.DialogID} does not exist.");
            }

            response.Dialog = dialog;

            _context.DialogResponses.Add(response);
            _context.SaveChanges();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
