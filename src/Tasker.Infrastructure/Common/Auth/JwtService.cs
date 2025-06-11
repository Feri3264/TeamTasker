using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using Tasker.Application.Common.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Tasker.Infrastructure.Common.Auth;

public class JwtService
    (IConfiguration configuration) : IJwtService
{
    private string TokenKey = configuration["Jwt:Key"];
    private static TimeSpan TokenExpiry = TimeSpan.FromHours(1);
    public string GenerateToken(Guid? userId, string? email)
    {
        var key = Encoding.UTF8.GetBytes(TokenKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenExpiry),
            signingCredentials: credentials,
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"]);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}