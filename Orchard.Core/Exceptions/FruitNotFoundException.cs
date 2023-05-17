namespace Orchard.Core.Exceptions;

public class FruitNotFoundException : Exception
{
    public FruitNotFoundException(string? message) : base(message)
    {
        
    }
}