namespace Orchard.Core.Services;

public class PasswordHashService
{
    public string HashPassword(string unhashedPassword)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedAndSaltedPassword = BCrypt.Net.BCrypt.HashPassword(unhashedPassword, salt);
        return hashedAndSaltedPassword;
    }

    public bool IsPasswordVerified(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}