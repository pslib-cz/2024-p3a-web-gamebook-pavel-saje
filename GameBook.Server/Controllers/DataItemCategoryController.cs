using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataItemCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DataItemCategoryController(AppDbContext context)
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

        [HttpPost]
        public ActionResult<DataItemCategory> Post(DataItemCategory category)
        {
            _context.ItemCategories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = category.CategoryID }, category);
        }

        [HttpPut("{id}")]
        public ActionResult<DataItemCategory> Put(int id, DataItemCategory category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<DataItemCategory> Delete(int id)
        {
            var category = _context.ItemCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.ItemCategories.Remove(category);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
