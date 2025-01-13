using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Models;
using GameBook.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


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
        public ActionResult<IEnumerable<Location>> Get()
        {
            return Ok(_context.Locations.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetId(int id, int energy)
        {
            if (energy <= 0) { return NotFound("YOU HAVE NO ENERGY YOU STUPID STINKY APE"); }
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpGet("GetNearestLocation/{locationId}")]
        public ActionResult<Location> GetNearestLocation(int locationId)
        {
            List<Location> NearBy = new List<Location>();

            var locations = _context.LocationPaths
                .Where(x => x.FirstNodeID == locationId || x.SecondNodeID == locationId)
                .ToList();

            var location = locations
                .Select(x => x.FirstNodeID == locationId ? x.SecondNodeID : x.FirstNodeID)
                .ToList();

            foreach(int loc in location)
            {
               var Near = _context.Locations.Find(loc);
                NearBy.Add(Near);
            }

            return Ok(NearBy);
        }



        [HttpPost]
        public ActionResult<Location> Post(Location location)
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

        [HttpGet("podlelokace/{locationId}")]
        public ActionResult<IEnumerable<LocationContent>> GetLocationContent(int locationId)
        {
            var locationContent = _context.LocationContent.Where(lc => lc.LocationID == locationId).ToList();
            if (locationContent == null || !locationContent.Any())
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
        public ActionResult<IEnumerable<LocationPath>> Get()
        {
            return Ok(_context.LocationPaths.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<LocationPath> GetId(int id)
        {
            var locationPaths = _context.LocationPaths.Find(id);
            if (locationPaths == null)
            {
                return NotFound();
            }
            return Ok(locationPaths);
        }



        [HttpPost]
        public ActionResult<LocationPath> Post(LocationPath locationPaths)
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

    [ApiController]
    [Route("api/[controller]")]
    public class RequiredItemsController : Controller
    {
        private AppDbContext _context;
        public RequiredItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<RequiredItems> Post(RequiredItems RequiredItems)
        {
            _context.RequiredItems.Add(RequiredItems);
            _context.SaveChanges();
            return Ok(RequiredItems);
        }


        [HttpGet("GetByLocation/{locationId}")]
        public async Task<ActionResult> GetRequiredByLocation(int locationId)
        {
            var RequiredItems = await _context.RequiredItems
                .Where(x => x.LocationID == locationId)
                .ToListAsync();

            return Ok(RequiredItems);
        }

    }

}
