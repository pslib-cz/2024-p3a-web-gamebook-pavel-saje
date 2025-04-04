using GameBook.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameBook.Server.Controllers
{
    public class UploadsController : Controller
    {
        [HttpGet("{path}")]
        public IActionResult GetImage(string path)
        {
            if (path == null) return BadRequest("Empty file path");

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploads = Path.Combine(webRootPath, "Uploads");

            var filePath = Path.Combine(uploads, path);
            var fullPath = Path.GetFullPath(filePath);

            if (!fullPath.StartsWith(Path.GetFullPath(uploads)))
            {
                return BadRequest("Invalid file path.");
            }

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound(fullPath);
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(fullPath, contentType);
        }
    }
}
