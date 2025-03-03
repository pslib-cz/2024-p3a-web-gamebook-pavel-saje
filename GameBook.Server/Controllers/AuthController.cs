using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly string key = "aTpqueLZr/AHwU0rDK0KTInroOaZszuwzGDShIaQW9U="; // Klíč pro šifrování JWT tokenu

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Simulovaná kontrola přihlašovacích údajů
        if (login.Username != "admin" || login.Password != "password")
        {
            return Unauthorized("Špatné přihlašovací údaje.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, "User") // Můžeme přidat role pro autorizaci
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString });
    }
}

// Model pro přihlášení
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
