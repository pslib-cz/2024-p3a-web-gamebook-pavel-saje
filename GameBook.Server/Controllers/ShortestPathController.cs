using GameBook.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortestPathController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ShortestPathController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int FirstLocationId, int SecondLocationId) 
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

            var result = Dijkstra(graph, FirstLocationId, SecondLocationId);
            return Ok(result);
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
    }
}
