using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slush.Data.Entity.Profile;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Slush.Services.JWT
{
    public class JWTService
    {
        private readonly JWTOptions _options;
        public JWTService(IOptions<JWTOptions> options)
        {
            _options = options.Value;
        }
        public String GenerateToken(User user)
        {
            Claim[] claims = { new Claim("userId", user.id.ToString()) };

            var signingCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiredHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }   
    }
}
