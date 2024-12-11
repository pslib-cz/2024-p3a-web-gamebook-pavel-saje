using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;


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
        public ActionResult<IEnumerable<Interactibles>> Get()
        {
            return Ok(_context.Interactibles.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Interactibles> GetId(int id)
        {
            var interactible = _context.Interactibles.Find(id);
            if (interactible == null)
            {
                return NotFound();
            }
            return Ok(interactible);
        }

        [HttpPost]
        public ActionResult<Interactibles> Post(Interactibles interactible)
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
        public ActionResult<IEnumerable<InteractibleOptions>> Get()
        {
            return Ok(_context.InteractibleOptions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<InteractibleOptions> GetId(int id)
        {
            var option = _context.InteractibleOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        [HttpPost]
        public ActionResult<InteractibleOptions> Post(InteractibleOptions option)
        {
            _context.InteractibleOptions.Add(option);
            _context.SaveChanges();
            return Ok(option);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var option = _context.InteractibleOptions.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.InteractibleOptions.Remove(option);
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
        public ActionResult<IEnumerable<OptionsEnum>> Get()
        {
            return Ok(_context.OptionsEnum.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<OptionsEnum> GetId(int id)
        {
            var option = _context.OptionsEnum.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        [HttpPost]
        public ActionResult<OptionsEnum> Post(OptionsEnum option)
        {
            _context.OptionsEnum.Add(option);
            _context.SaveChanges();
            return Ok(option);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var option = _context.OptionsEnum.Find(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.OptionsEnum.Remove(option);
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
        public ActionResult<IEnumerable<NpcDialog>> Get()
        {
            return Ok(_context.NpcDialog.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<NpcDialog> GetId(int id)
        {
            var dialog = _context.NpcDialog.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }
            return Ok(dialog);
        }

        [HttpPost]
        public ActionResult<NpcDialog> Post(NpcDialog dialog)
        {
            _context.NpcDialog.Add(dialog);
            _context.SaveChanges();
            return Ok(dialog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dialog = _context.NpcDialog.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }

            _context.NpcDialog.Remove(dialog);
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
        public ActionResult<IEnumerable<NpcDialogResponses>> Get()
        {
            return Ok(_context.NpcDialogResponses.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<NpcDialogResponses> GetId(int id)
        {
            var response = _context.NpcDialogResponses.Find(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<NpcDialogResponses> Post(NpcDialogResponses response)
        {
            _context.NpcDialogResponses.Add(response);
            _context.SaveChanges();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _context.NpcDialogResponses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            _context.NpcDialogResponses.Remove(response);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
