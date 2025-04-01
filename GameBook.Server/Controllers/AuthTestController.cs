using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/test")]
[ApiController]
public class AuthTestController : ControllerBase
{
    [Authorize] // Tohle zajistí, že pouze ověřený uživatel může volat tuto metodu
    [NonAction][HttpPost("secure")]
    public IActionResult SecurePost([FromBody] TestModel model)
    {
        return Ok(new { message = "Úspěšně ověřený požadavek!", data = model });
    }

    [AllowAnonymous] // Tento endpoint je veřejný
    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok("Tento endpoint je veřejný.");
    }
}

// Model pro testovací POST request
public class TestModel
{
    public string Data { get; set; }
}
