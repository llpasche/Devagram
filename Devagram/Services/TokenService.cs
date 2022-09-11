using Devagram.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Devagram.Services
{
    public class TokenService
    {
        public static string SetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var cryptoKey = Encoding.ASCII.GetBytes(JWTKey.Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(cryptoKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
