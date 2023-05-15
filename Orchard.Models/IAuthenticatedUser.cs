namespace Orchard.Models;

public interface IAuthenticatedUser
{
    int Id { get; }
    string Email { get; }
}