using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
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
                var key = Encoding.Unicode.GetBytes(EnvironmentVariables.JWT_SECRET);

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
                    claims.Add(new Claim(claim, claim));
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

                var encodedJwt = tokenHandler.CreateEncodedJwt(tokenDescriptor);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var x = tokenHandler.WriteToken(token);

                return x;
            }

            return string.Empty;
        }

        public static bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(EnvironmentVariables.JWT_SECRET);
            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;
                if (string.IsNullOrEmpty(jwtToken.Claims.First(x => x.Type == "UserId").Value))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
