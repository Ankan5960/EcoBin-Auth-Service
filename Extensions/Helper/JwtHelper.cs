using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Auth_Api.Extensions.Helpers;
using Auth_Api.Model.DTOs;

namespace Auth_Api.Helpers;
public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(AuthDto authDto, int expireTimeInMinutes = 5)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if (jwtKey == null)
        {
            throw new InvalidOperationException("Jwt:Key is missing from configuration");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, authDto.UserId.ToString()),
            new Claim("userName", authDto.UserName),
            new Claim(ClaimTypes.Email, authDto.Email),
            new Claim("firstName", authDto.FirstName),
            new Claim("lastName", authDto.LastName ?? string.Empty),
            new Claim("roleId", authDto.RoleId.ToString()),
            new Claim("roleName", authDto.RoleName)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireTimeInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[64];
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
