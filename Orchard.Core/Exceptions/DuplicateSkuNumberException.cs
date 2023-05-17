namespace Orchard.Core.Exceptions;

public class DuplicateSkuNumberException : Exception
{
    public DuplicateSkuNumberException(string? message) : base(message)
    {
    }
}