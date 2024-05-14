using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OurForum.Backend.Extensions;
using OurForum.Backend.Identity;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services
{
    public class AuthService
    {
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

        public static string Authenticate(string email, string password)
        {
            using var dbContext = new DatabaseContext();
            var userService = new UserService(dbContext);
            var user = userService.GetByEmail(email);

            if (user is not null && HashMan.Verify(password, user.HashedPassword))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_KEY);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, user.Email),
                    new(JwtRegisteredClaimNames.Email, user.Email),
                    new(CustomClaims.USER_ID, user.Id.ToString()),
                    new(CustomClaims.ROLE_ID, user.Role?.Id.ToString() ?? string.Empty),
                };

                foreach (var claim in user.Role?.Permissions.Split(";") ?? [])
                {
                    var permissionProperties = typeof(Permissions).HasField(claim);
                    if (permissionProperties)
                    {
                        claims.Add(new Claim(claim, claim));
                    }
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = EnvironmentVariables.JWT_AUDIENCE,
                    Issuer = EnvironmentVariables.JWT_ISSUER,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(TokenLifetime),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                return tokenHandler.CreateEncodedJwt(tokenDescriptor);
            }

            return string.Empty;
        }
    }
}
