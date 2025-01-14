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
        public ActionResult<Location> Post([FromForm] Location location)
        {
            if (location.BackgroundImage == null || location.BackgroundImage.Length == 0) {
                return BadRequest("BackgroundImage not provided.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Locations");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // rename file
            string fileExtension = Path.GetExtension(location.BackgroundImage.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(location.BackgroundImage.FileName);
            uniqueFileName = string.Join("_", uniqueFileName.Split(Path.GetInvalidFileNameChars())); // Sanitize file name

            location.BackgroundImagePath = Path.Combine("/Uploads/Locations", uniqueFileName).Replace("\\", "/");

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    location.BackgroundImage.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


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


            if (location.BackgroundImagePath != null)
            {
                //temporary solution - Path.Combile() requires directories to be separated
                char separator = Path.AltDirectorySeparatorChar;
                string fileLocation = Directory.GetCurrentDirectory() + separator + "wwwroot" + separator + location.BackgroundImagePath;
                
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
            var location = _context.Locations.Find(locationContent.LocationID);
            var interactible = _context.Interactibles.Find(locationContent.InteractibleID);

            if (location == null)
            {
                return BadRequest($"Location with ID {locationContent.LocationID} does not exist.");
            }
            if (interactible == null)
            {
                return BadRequest($"Interactible with ID {locationContent.InteractibleID} does not exist.");
            }

            locationContent.Location = location;
            locationContent.Interactible = interactible;

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
            var Node1 = _context.Locations.Find(locationPaths.FirstNodeID);
            var Node2 = _context.Locations.Find(locationPaths.SecondNodeID);

            if (Node1 == null)
            {
                return BadRequest($"Location with ID {locationPaths.FirstNodeID} does not exist.");
            }
            if (Node2 == null)
            {
                return BadRequest($"Location with ID {locationPaths.SecondNodeID} does not exist.");
            }

            locationPaths.FirstNode = Node1;
            locationPaths.SecondNode = Node2;

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
        public ActionResult<RequiredItems> Post([FromForm] RequiredItems requiredItems)
        {
            if (requiredItems.LocationID <= 0 || requiredItems.ItemID <= 0)
            {
                return BadRequest("Invalid LocationID or ItemID provided.");
            }

            // Optional: Pokud máte připojený soubor (například obrázek) jako součást RequiredItems, můžete to zde ošetřit.
            // Například pokud by RequiredItems obsahovalo pole BackgroundImage:
            /*
            if (requiredItems.BackgroundImage == null || requiredItems.BackgroundImage.Length == 0)
            {
                return BadRequest("BackgroundImage not provided.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "RequiredItems");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileExtension = Path.GetExtension(requiredItems.BackgroundImage.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(requiredItems.BackgroundImage.FileName);
            uniqueFileName = string.Join("_", uniqueFileName.Split(Path.GetInvalidFileNameChars())); // Sanitize file name

            requiredItems.BackgroundImagePath = Path.Combine("/Uploads/RequiredItems", uniqueFileName).Replace("\\", "/");

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    requiredItems.BackgroundImage.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            */

            try
            {
                _context.RequiredItems.Add(requiredItems);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(requiredItems);
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
