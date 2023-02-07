using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.v1.Services;

public class JwtService
{
    
    private readonly IConfiguration _config;
    
    public JwtService(IConfiguration config) =>
        _config = config;
    public string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, username),
        };

        // Use Dotnet's Secret Manager to store the Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:Key"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddSeconds(Double.Parse(_config["Authentication:ExpirationTimeInSeconds"] ?? "3600")),
            Issuer = _config["Authentication:Schemes:Bearer:ValidIssuer"],
            Audience = _config["Authentication:Schemes:Bearer:ValidAudience"],
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}
