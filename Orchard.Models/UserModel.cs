using System.ComponentModel.DataAnnotations;

namespace Orchard.Models;

public class UserModel
{
    public int Id { get; set; }
    [Required, EmailAddress, MaxLength(512)]
    public string Email { get; set; }
    [Required, MaxLength(64)]
    public string FirstName { get; set; }
    [Required, MaxLength(64)]
    public string LastName { get; set; }
    [Required, 
     RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,23}$")]
    public string Password { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? EmailVerificationGuid { get; set; }
}