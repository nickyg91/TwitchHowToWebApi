namespace Orchard.Core.Exceptions;

public class UnauthorizedBasketAccessException : Exception
{
    public UnauthorizedBasketAccessException(string? message) : base(message)
    {
    }
}