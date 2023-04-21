using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Orchard.Models.Authentication;
using Orchard.Services.Domain;
using Orchard.Web.Configuration;

namespace Orchard.Web.Authentication;

public class TokenGenerator
{
    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;
    public TokenGenerator(IOptions<JwtSettings> jwtSettings, IUserService userService)
    {
        _userService = userService;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<JwtModel?> GenerateToken(LoginModel model)
    {
        var user = await _userService.Authenticate(model);
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
        var jwtToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience, 
            new List<Claim>
            {
                new("userId", user.Id.ToString()),
                new("email", user.Email)
            },
            null,
            DateTime.UtcNow.AddMinutes(30),
            signingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtModel
        {
            Token = tokenString,
            Claims = jwtToken.Claims.ToList()
        };
    }
}