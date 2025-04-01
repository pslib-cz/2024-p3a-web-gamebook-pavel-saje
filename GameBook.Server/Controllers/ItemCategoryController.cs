using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ItemCategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataItemCategory>> Get()
        {
            return Ok(_context.ItemCategories.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DataItemCategory> Get(int id)
        {
            var category = _context.ItemCategories
                .Include(c => c.Items)
                .FirstOrDefault(c => c.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [NonAction][HttpPost]
        public async Task<ActionResult<ViewItemCategory>> PostCategory(InputItemCategory category)
        {
            var dataCategory = new DataItemCategory
            {
                Name = category.Name
            };

            _context.ItemCategories.Add(dataCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dataCategory.CategoryID }, dataCategory);

        }

        [NonAction][HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _context.ItemCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.ItemCategories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
