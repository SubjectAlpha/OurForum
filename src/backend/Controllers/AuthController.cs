using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    [HttpPost("token")]
    [AllowAnonymous]
    public IActionResult GenerateToken([FromBody] TokenGenerationRequest body)
    {
        using var dbContext = new DatabaseContext();
        var user = dbContext.Users.FirstOrDefault(x => x.Email == body.Email);

        if (user is not null)
        {
            var hashedPassword = HashMan.HashString(body.Password);
            if (HashMan.Verify(body.Password, user.EncryptedPassword)) 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.Unicode.GetBytes(EnvironmentVariables.JWT_SECRET);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, user.Email),
                    new(JwtRegisteredClaimNames.Email, user.Email),
                };

                foreach (var claim in user.Role.Claims.Split(";"))
                {
                    claims.Add(new Claim(claim, claim));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = EnvironmentVariables.JWT_AUDIENCE,
                    Issuer = EnvironmentVariables.JWT_ISSUER,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(TokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var encodedJwt = 
                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenHandler.WriteToken(token);

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        else
        {
            return NotFound();
        }
    }

    public struct TokenGenerationRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
