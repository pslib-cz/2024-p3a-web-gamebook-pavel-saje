using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Models;
using GameBook.Server.Data;


namespace GameBook.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private AppDbContext _context;
        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Locations>> Get()
        {
            return Ok(_context.Locations.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Locations> GetId(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }



        [HttpPost]
        public ActionResult<Locations> Post(Locations location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
            return Ok(location);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            _context.SaveChanges();
            return NoContent();
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class LocationContentController : Controller
    {

        private AppDbContext _context;
        public LocationContentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LocationContent>> Get()
        {
            return Ok(_context.LocationContent.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<LocationContent> GetId(int id)
        {
            var locationContent = _context.LocationContent.Find(id);
            if (locationContent == null)
            {
                return NotFound();
            }
            return Ok(locationContent);
        }



        [HttpPost]
        public ActionResult<LocationContent> Post(LocationContent locationContent)
        {
            _context.LocationContent.Add(locationContent);
            _context.SaveChanges();
            return Ok(locationContent);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var locationContent = _context.LocationContent.Find(id);
            if (locationContent == null)
            {
                return NotFound();
            }

            _context.LocationContent.Remove(locationContent);
            _context.SaveChanges();
            return NoContent();
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class LocationPathsController : Controller
    {

        private AppDbContext _context;
        public LocationPathsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LocationPaths>> Get()
        {
            return Ok(_context.LocationPaths.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<LocationPaths> GetId(int id)
        {
            var locationPaths = _context.LocationPaths.Find(id);
            if (locationPaths == null)
            {
                return NotFound();
            }
            return Ok(locationPaths);
        }



        [HttpPost]
        public ActionResult<LocationPaths> Post(LocationPaths locationPaths)
        {
            _context.LocationPaths.Add(locationPaths);
            _context.SaveChanges();
            return Ok(locationPaths);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var locationPaths = _context.LocationPaths.Find(id);
            if (locationPaths == null)
            {
                return NotFound();
            }

            _context.LocationPaths.Remove(locationPaths);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
