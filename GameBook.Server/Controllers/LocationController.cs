using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBook.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameBook.Server.Data;
using PowerArgs;

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
            var locations = await _context.Locations
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
        public async Task<ActionResult<ViewLocation>> GetLocationById(int id, int currentId)
        {
            var paths = await _context.LocationPaths.ToListAsync();

            var graph = new Dictionary<int, List<(int, int)>>();

            foreach (var path in paths)
            {
                if (!graph.ContainsKey(path.FirstNodeID))
                    graph[path.FirstNodeID] = new List<(int, int)>();
                if (!graph.ContainsKey(path.SecondNodeID))
                    graph[path.SecondNodeID] = new List<(int, int)>();

                graph[path.FirstNodeID].Add((path.SecondNodeID, path.EnergyTravelCost));
                graph[path.SecondNodeID].Add((path.FirstNodeID, path.EnergyTravelCost));
            }

            var FirstLocationId = currentId != 0 ? currentId : id;

            var result = Dijkstra(graph, FirstLocationId, id);

            var EndFetch = await _context.Ends
                .Where(e => e.LocationID == id)
                .Select(e => new ViewEnd
                {
                    EndID = e.EndID,
                    DialogID = e.DialogID,
                    LocationID = e.LocationID
                })
                .FirstOrDefaultAsync();

            // Nejprve načteme data bez volání ConvertImageToBase64
            var location = await _context.Locations
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
                    End = EndFetch == null ? null : new List<ViewEnd> { EndFetch },
                    
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
                    }).ToList(),


                    travelCost = result,

                    LocationContents = l.LocationContents.Select(lc => new ViewLocationContent
                    {
                        LocationContentID = lc.LocationContentID,
                        InteractibleID = lc.InteractibleID,
                        Interactible = new ViewInteractible
                        {
                            InteractibleID = lc.Interactible.InteractibleID,
                            Name = lc.Interactible.Name,
                            ImagePath = lc.Interactible.ImagePath,
                        },
                        XPos = lc.XPos,
                        YPos = lc.YPos,
                        size = lc.size
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        private int Dijkstra(Dictionary<int, List<(int, int)>> graph, int start, int end)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int?>();
            var pq = new SortedSet<(int, int)>();

            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
                previous[node] = null;
            }

            distances[start] = 0;
            pq.Add((0, start));

            while (pq.Count > 0)
            {
                var (currentDistance, current) = pq.Min;
                pq.Remove((currentDistance, current));

                if (current == end) break;

                foreach (var (neighbor, weight) in graph[current])
                {
                    int newDist = currentDistance + weight;
                    if (newDist < distances[neighbor])
                    {
                        pq.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = newDist;
                        previous[neighbor] = current;
                        pq.Add((newDist, neighbor));
                    }
                }
            }

            return distances[end];
        }



    [HttpGet("{id}/connected")]
        public async Task<ActionResult<List<ViewLocation>>> GetConnectedLocations(int id)
        {
            var locationPaths = await _context.LocationPaths
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
                        .Select(lc => new ViewLocationContent
                        {
                            LocationContentID = lc.LocationContentID,
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
            var locationPaths = await _context.LocationPaths
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