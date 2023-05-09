using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Orchard.Data.Cache;
using Orchard.Models;
using Orchard.Models.Authentication;
using Orchard.Services.Domain;
using Orchard.Web.Configuration;

namespace Orchard.Web.Authentication;

public class TokenGenerator
{
    private readonly IUserService _userService;
    private readonly ICacheService _cache;
    private readonly JwtSettings _jwtSettings;
    public TokenGenerator(IOptions<JwtSettings> jwtSettings, IUserService userService, ICacheService cache)
    {
        _userService = userService;
        _cache = cache;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<JwtModel?> GenerateToken(LoginModel model)
    {
        var user = await _userService.Authenticate(model);
        return await GenerateJwt(user);
    }

    public async Task<JwtModel?> RefreshToken(string refreshToken)
    {
        var tokenExists = _cache.Database.KeyExists(refreshToken);
        if (!tokenExists)
        {
            return null;
        }

        if (!int.TryParse(await _cache.Database.StringGetAsync(refreshToken), out var userId))
        {
            return null;
        }
        await _cache.Database.KeyDeleteAsync(refreshToken);
        var user = await _userService.GetUserById(userId);
        return await GenerateJwt(user);
    }

    private async Task<JwtModel> GenerateJwt(UserModel user)
    {
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

        var refreshToken = Guid.NewGuid().ToString("N");
        await _cache.Database.StringSetAsync(refreshToken, user.Id, TimeSpan.FromMinutes(60));
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return new JwtModel
        {
            Token = token,
            Claims = jwtToken.Claims.ToList(),
            RefreshToken = refreshToken
        };
    }
}