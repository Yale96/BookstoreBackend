using System.Security.Claims;
using BookstoreBackend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreBackend.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            // Voor deze demo gebruik ik een hardcoded user
            // Het is mogelijk om een register endpoint te maken en gebruikers op te slaan in de database, maar voor nu wil ik graag alles binnen een schappelijke tijd afkrijgen
            // Daarnaast zou het netjes zijn om de logica te verplaatsen naar een apparte service en deze los te testen, maar ik maak enkel gebruik van een login methode, dus laat ik het voor nu zo staan.
            // Er dient behoorlijk wat gemockt te worden om dit te testen, dus hier ga ik op mijn gemak nog even naar kijken.
            if (model.Username == "admin" && model.Password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Opslaan van de sessie ook met browser herstarts
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20) // Na 20 minuten verloopt de sessie
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid login" });
        }
    }
}
