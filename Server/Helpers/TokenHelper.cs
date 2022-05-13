using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Server.Helpers;

public class TokenHelper
{
    private readonly byte[] secretKey;

    public TokenHelper(byte[] secretkey)
    {
        secretKey = secretkey;
    }

    public string GenerateToken(string userName, string userId)
    {
        var utcNow = DateTime.UtcNow;

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userName),
            new(ClaimTypes.UserData, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, utcNow.ToString())
        };

        var expiredDateTime = utcNow.AddDays(5);

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var symmetric = new SymmetricSecurityKey(secretKey);

        var signingCredentials = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);

        return jwtSecurityTokenHandler.WriteToken(new JwtSecurityToken(claims: claims, expires: expiredDateTime,
            notBefore: utcNow, signingCredentials: signingCredentials));
    }
}