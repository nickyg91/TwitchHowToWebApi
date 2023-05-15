namespace Orchard.Data.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? EmailVerificationGuid { get; set; }
    public ICollection<Basket> Baskets { get; set; }
}