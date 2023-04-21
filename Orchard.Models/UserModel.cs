namespace Orchard.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? EmailVerificationGuid { get; set; }
}