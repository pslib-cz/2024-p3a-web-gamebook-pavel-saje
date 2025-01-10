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
        public ActionResult<Interactible> Post(Interactible interactible)
        {
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
        public ActionResult<InteractiblesOption> Post(InteractiblesOption option)
        {
            _context.InteractiblesOptions.Add(option);
            _context.SaveChanges();
            return Ok(option);
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
            _context.InteractiblesItems.Add(interactibleItem);
            _context.SaveChanges();
            return Ok(interactibleItem);
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
