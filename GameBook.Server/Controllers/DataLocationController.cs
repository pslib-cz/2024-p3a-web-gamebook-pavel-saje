using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBook.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameBook.Server.Data;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewLocation>>> GetLocations()
        {
            var locations = await _context.Location
                .Include(l => l.LocationContents)
                .Include(l => l.RequiredItems)
                    .ThenInclude(ri => ri.Item)
                .Select(l => new ViewLocation
                {
                    LocationID = l.LocationID,
                    Name = l.Name,
                    BackgroundImagePath = l.BackgroundImagePath,
                    RadiationGain = l.RadiationGain,
                    LocationContents = l.LocationContents.Select(lc => new ViewLocationContent
                    {
                        InteractibleID = lc.InteractibleID,
                        Interactible = new ViewInteractible
                        {
                            InteractibleID = lc.Interactible.InteractibleID,
                            Name = lc.Interactible.Name,
                            ImagePath = lc.Interactible.ImagePath
                        },
                        XPos = lc.XPos,
                        YPos = lc.YPos
                    }).ToList(),
                    RequiredItems = l.RequiredItems.Select(ri => new ViewItem
                    {
                        ItemID = ri.Item.ItemID,
                        Name = ri.Item.Name,
                        TradeValue = ri.Item.TradeValue,
                        Stackable = ri.Item.Stackable,
                        CategoryId = ri.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ri.Item.Category.CategoryID,
                            Name = ri.Item.Category.Name
                        }
                    }).ToList()
                }).ToListAsync();

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewLocation>> GetLocationById(int id)
        {
            var location = await _context.Location
                .Include(l => l.LocationContents)
                .Include(l => l.RequiredItems)
                    .ThenInclude(ri => ri.Item)
                .Where(l => l.LocationID == id)
                .Select(l => new ViewLocation
                {
                    LocationID = l.LocationID,
                    Name = l.Name,
                    BackgroundImagePath = l.BackgroundImagePath,
                    RadiationGain = l.RadiationGain,
                    LocationContents = l.LocationContents.Select(lc => new ViewLocationContent
                    {
                        InteractibleID = lc.InteractibleID,
                        Interactible = new ViewInteractible
                        {
                            InteractibleID = lc.Interactible.InteractibleID,
                            Name = lc.Interactible.Name,
                            ImagePath = lc.Interactible.ImagePath
                        },
                        XPos = lc.XPos,
                        YPos = lc.YPos
                    }).ToList(),
                    RequiredItems = l.RequiredItems.Select(ri => new ViewItem
                    {
                        ItemID = ri.Item.ItemID,
                        Name = ri.Item.Name,
                        TradeValue = ri.Item.TradeValue,
                        Stackable = ri.Item.Stackable,
                        CategoryId = ri.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ri.Item.Category.CategoryID,
                            Name = ri.Item.Category.Name
                        }
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpGet("{id}/connected")]
        public async Task<ActionResult<List<ViewLocation>>> GetConnectedLocations(int id)
        {
            var locationPaths = await _context.LocationPath
                .Where(lp => lp.FirstNodeID == id || lp.SecondNodeID == id)
                .Include(lp => lp.FirstNode).ThenInclude(l => l.LocationContents).ThenInclude(lc => lc.Interactible)
                .Include(lp => lp.FirstNode).ThenInclude(l => l.RequiredItems).ThenInclude(ri => ri.Item).ThenInclude(i => i.Category)
                .Include(lp => lp.SecondNode).ThenInclude(l => l.LocationContents).ThenInclude(lc => lc.Interactible)
                .Include(lp => lp.SecondNode).ThenInclude(l => l.RequiredItems).ThenInclude(ri => ri.Item).ThenInclude(i => i.Category)
                .ToListAsync();

            var connectedLocations = locationPaths.Select(lp => lp.FirstNodeID == id ? lp.SecondNode : lp.FirstNode)
                .Select(l => new ViewLocation
                {
                    LocationID = l.LocationID,
                    Name = l.Name,
                    BackgroundImagePath = l.BackgroundImagePath,
                    RadiationGain = l.RadiationGain,
                    LocationContents = l.LocationContents
                        .Where(lc => lc.Interactible != null)
                        .Select(lc => new ViewLocationContent
                        {
                            InteractibleID = lc.InteractibleID,
                            Interactible = new ViewInteractible
                            {
                                InteractibleID = lc.Interactible.InteractibleID,
                                Name = lc.Interactible.Name,
                                ImagePath = lc.Interactible.ImagePath
                            },
                            XPos = lc.XPos,
                            YPos = lc.YPos
                        }).ToList(),
                    RequiredItems = l.RequiredItems.Select(ri => new ViewItem
                    {
                        ItemID = ri.Item.ItemID,
                        Name = ri.Item.Name,
                        TradeValue = ri.Item.TradeValue,
                        Stackable = ri.Item.Stackable,
                        CategoryId = ri.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ri.Item.Category.CategoryID,
                            Name = ri.Item.Category.Name
                        }
                    }).ToList()
                }).ToList();

            if (!connectedLocations.Any())
            {
                return NotFound();
            }

            return Ok(connectedLocations);
        }


        [HttpGet("connections")]
        public async Task<ActionResult<List<ViewLocationPath>>> GetLocationConnections()
        {
            var locationPaths = await _context.LocationPath
                .Include(lp => lp.FirstNode)
                .ThenInclude(l => l.LocationContents)
                .Include(lp => lp.FirstNode)
                .ThenInclude(l => l.RequiredItems)
                .ThenInclude(ri => ri.Item)
                .ThenInclude(i => i.Category)
                .Include(lp => lp.SecondNode)
                .ThenInclude(l => l.LocationContents)
                .Include(lp => lp.SecondNode)
                .ThenInclude(l => l.RequiredItems)
                .ThenInclude(ri => ri.Item)
                .ThenInclude(i => i.Category)
                .ToListAsync();

            var viewLocationPaths = locationPaths.Select(lp => new ViewLocationPath
            {
                PathID = lp.PathID,
                FirstNodeID = lp.FirstNodeID,
                SecondNodeID = lp.SecondNodeID,
                EnergyTravelCost = lp.EnergyTravelCost,
                FirstNode = new ViewLocation
                {
                    LocationID = lp.FirstNode.LocationID,
                    Name = lp.FirstNode.Name,
                    BackgroundImagePath = lp.FirstNode.BackgroundImagePath,
                    RadiationGain = lp.FirstNode.RadiationGain,
                    LocationContents = lp.FirstNode.LocationContents.Select(lc => new ViewLocationContent
                    {
                        InteractibleID = lc.InteractibleID,
                        XPos = lc.XPos,
                        YPos = lc.YPos
                    }).ToList(),
                    RequiredItems = lp.FirstNode.RequiredItems.Select(ri => new ViewItem
                    {
                        ItemID = ri.Item.ItemID,
                        Name = ri.Item.Name,
                        TradeValue = ri.Item.TradeValue,
                        Stackable = ri.Item.Stackable,
                        CategoryId = ri.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ri.Item.Category.CategoryID,
                            Name = ri.Item.Category.Name
                        }
                    }).ToList()
                },
                SecondNode = new ViewLocation
                {
                    LocationID = lp.SecondNode.LocationID,
                    Name = lp.SecondNode.Name,
                    BackgroundImagePath = lp.SecondNode.BackgroundImagePath,
                    RadiationGain = lp.SecondNode.RadiationGain,
                    LocationContents = lp.SecondNode.LocationContents.Select(lc => new ViewLocationContent
                    {
                        InteractibleID = lc.InteractibleID,
                        XPos = lc.XPos,
                        YPos = lc.YPos
                    }).ToList(),
                    RequiredItems = lp.SecondNode.RequiredItems.Select(ri => new ViewItem
                    {
                        ItemID = ri.Item.ItemID,
                        Name = ri.Item.Name,
                        TradeValue = ri.Item.TradeValue,
                        Stackable = ri.Item.Stackable,
                        CategoryId = ri.Item.CategoryId,
                        Category = new ViewItemCategory
                        {
                            CategoryID = ri.Item.Category.CategoryID,
                            Name = ri.Item.Category.Name
                        }
                    }).ToList()
                }
            }).ToList();

            return Ok(viewLocationPaths);
        }
    }
}