namespace Orchard.Data.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAdUtc { get; set; }
}