using System.Security.Claims;

namespace Orchard.Models.Authentication;

public class JwtModel
{
    public string Token { get; set; }
    public List<Claim> Claims { get; set; }
    public string RefreshToken { get; set; }
}