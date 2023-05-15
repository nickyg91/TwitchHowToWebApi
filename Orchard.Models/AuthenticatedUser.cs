using System.Security.Claims;

namespace Orchard.Models;

public class AuthenticatedUser : IAuthenticatedUser
{
    private readonly ClaimsPrincipal _user;
    public AuthenticatedUser(ClaimsPrincipal user)
    {
        _user = user;
    }

    public int Id => int.Parse(_user.Claims.First(x => x.Type == "userId").Value);
    public string Email => _user.Claims.First(x => x.Type == "email").Value;
}