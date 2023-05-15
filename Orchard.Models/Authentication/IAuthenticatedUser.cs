namespace Orchard.Models.Authentication;

public interface IAuthenticatedUser
{
    int Id { get; }
    string Email { get; }
}